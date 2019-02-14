using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitbasket : MonoBehaviour {
    //フルーツをいれておくリスト
    public List<GameObject> fruit = new List<GameObject>();

    //フルーツが取られたときに追加するフルーツ配列
    public GameObject[] fruty;
    
    //フルーツの新しい湧き位置
    public Vector3[] f_pos;

    // Use this for initialization
    void Start () {
		
	}
	
	void Update ()
    {
        //切り株の上のフルーツが4未満のとき
       if(fruit.Count < 4)
        {
            //fruty配列から新たにフルーツを生成
            GameObject FRUIT = Instantiate(fruty[Random.Range(0, fruty.Length)]);

            //生成する位置を決定
            FRUIT.transform.position = (f_pos[Random.Range(0, f_pos.Length)]);

            //生成したフルーツを切り株の子要素にする
            FRUIT.transform.parent = gameObject.transform;

            //生成したフルーツをfruitリストに追加する
            fruit.Add(FRUIT);            
        }
	}

    private void OnTriggerEnter(Collider other)
    {
       //Enemyタグがついた敵が切り株に触れ、フルーツが1個以上ある時
       if (other.gameObject.tag == "Enemy" && 0 < fruit.Count )
        {
       　    //GameObject Enemy = GameObject.FindWithTag("Enemy");

             //fruitリストの0番目のフルーツを触れた敵の子要素にする
             fruit[0].transform.parent = other.transform;

             //fruitリストの0番目からフルーツを取り除く
             fruit.RemoveAt(0);
        }
    }
}
