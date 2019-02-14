using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Avatar;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    private Transform UIcamTrans;
    public AudioSource audiosource;
    public GameObject Bullet;
    public bool Right = false;
    public Vector3[] Torques = { new Vector3(30, 30, 50), new Vector3(30, -30, 30), new Vector3(30, 70, 40) };


    [SerializeField]
    private Image aimPointImage;
    RaycastHit hit;

    void Start()
    {
        audiosource = gameObject.GetComponent<AudioSource>();
        UIcamTrans = GameObject.Find("UICamera").GetComponent<Transform>();
    }

    void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);//Rayをとばす（発射座標、向き）

        if (Physics.Raycast(ray, out hit, 30.0f))
        {

            // Rayがhitしたオブジェクトのタグ名を取得
            string hitTag = hit.collider.tag;


            // タグの名前がEnemyだったら、照準の色が変わる
            if ((hitTag.Equals("Enemy")))
            {
                aimPointImage.color = new Color(1, 0, 0, 1);
            }
            else if ((hitTag.Equals("Boss")))
            {
                aimPointImage.color = new Color(1, 0, 0, 1);
            }
            else
            {
                //Enemy以外では水色に
                aimPointImage.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            }
            //Debug.DrawRay(transform.position, ray.direction * 30f, Color.green, 0.1f, false);
            //aim位置の更新
            aimPointImage.transform.position = new Vector3(hit.point.x * -1, hit.point.y, hit.point.z * -1);
            aimPointImage.transform.LookAt(UIcamTrans);
        }

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {

            if (!Right) { return; }
            audiosource.Play();
            Shoot(hit.point);
        }

        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            {
                if (Right) { return; }
                audiosource.Play();
                Shoot(hit.point);
            }
        }
    }


        void OnShot(Vector3 aimpos)
        {
            GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            Rigidbody bullet_rb = bullet.GetComponent<Rigidbody>();
            bullet_rb.rotation = transform.rotation;
            bullet_rb.AddRelativeForce(0, 30, 500);
        }

         void Shoot(Vector3 aimPos)//角度指定で斜方投射
        {
            ShootFixedAngle(aimPos, 15);
        }

        void ShootFixedAngle(Vector3 aimpos, float aimangle)
        {
            float speedVec = ComputeVectorFromAngle(aimpos, aimangle);
            if (speedVec <= 0.0f)
            {
                Debug.Log("!!");
                OnShot(aimpos);
                return;
            }
            Vector3 vec = ConvertVectorToVector3(speedVec, aimangle, aimpos);
            InstantiateBullet(vec);
        }

         float ComputeVectorFromAngle(Vector3 aimpos, float aimangle)//角度処理
        {
            Vector2 startPos = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPos = new Vector2(aimpos.x, aimpos.z);
            float distance = Vector2.Distance(targetPos, startPos);

            float x = distance;
            float g = Physics.gravity.y;
            float first_y = transform.position.y;
            float y = aimpos.y;

            float rad = aimangle * Mathf.Deg2Rad;//ラジアンに変換

            float cos = Mathf.Cos(rad);
            float tan = Mathf.Tan(rad);

            float first_vSquare = g * x * x / (2 * cos * cos * (y - first_y - x * tan));
            if (first_vSquare <= 0)
            {
                return 0;
            }

            float first_v = Mathf.Sqrt(first_vSquare);
            return first_v;
        }

         Vector3 ConvertVectorToVector3(float first_v, float first_angle, Vector3 aimpos)
        {
            Vector3 startPos = transform.position;
            Vector3 targetPos = aimpos;
            startPos.y = 0.0f;
            targetPos.y = 0.0f;

            Vector3 dir = (targetPos - startPos).normalized;
            Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
            Vector3 vec = first_v * Vector3.right;

            vec = yawRot * Quaternion.AngleAxis(first_angle, Vector3.forward) * vec;

            return vec;
        }

         void InstantiateBullet(Vector3 aimVector)//弾の発射
        {
            if (Bullet == null)
            {
                throw new System.NullReferenceException("Bullet is null");
            }
            var obj = Instantiate<GameObject>(Bullet, transform.position,
                Quaternion.identity);
            var rigidbody = obj.GetComponent<Rigidbody>();

            Vector3 force = aimVector * rigidbody.mass;
            rigidbody.AddForce(force, ForceMode.Impulse);
            rigidbody.AddTorque(Torques[Random.Range(0, Torques.Length)] * 1 * Mathf.PI, ForceMode.Impulse);
        }

        void ShootFixedTime(Vector3 aimPos, float airTime)
        {
            float speedVec = ComputeVectorFormTime(aimPos, airTime);
            float angle = ComputeVectorFromAngle(aimPos, airTime);

            if (speedVec <= 0.0f)
            {
                Debug.Log("!!");
                return;
            }

            Vector3 vec = ConvertVectorToVector3(speedVec, angle, aimPos);
            InstantiateBullet(vec);
        }

         float ComputeVectorFormTime(Vector3 aimPos, float airTime)
        {
            Vector2 vec = ComputeVectorXYFromTime(aimPos, airTime);

            float v_x = vec.x;
            float v_y = vec.y;

            float first_vSquare = v_x * v_x + v_y * v_y;
            if (first_vSquare <= 0.0f)
            {
                return 0.0f;
            }

            float first_v = Mathf.Sqrt(first_vSquare);
            return first_v;
        }

        float ComputeAngleFromTime(Vector3 aimPos, float airTime)
        {
            Vector2 vec = ComputeVectorXYFromTime(aimPos, airTime);
            float v_x = vec.x;
            float v_y = vec.y;

            float rad = Mathf.Atan2(v_y, v_x);
            float angle = rad * Mathf.Rad2Deg;

            return angle;
        }

        Vector2 ComputeVectorXYFromTime(Vector3 aimPos, float airTime)
        {
            if (airTime <= 0.0f)
            {
                return Vector2.zero;
            }

            Vector2 startPos = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPos = new Vector2(aimPos.x, aimPos.z);
            float distance = Vector2.Distance(targetPos, startPos);

            float x = distance;
            float g = -Physics.gravity.y;
            float first_y = transform.position.y;
            float y = aimPos.y;
            float t = airTime;

            float v_x = x / t;
            float v_y = (y - first_y) / t + (g * t) / 2;

            return new Vector2(v_x, v_y);
        }
    }

