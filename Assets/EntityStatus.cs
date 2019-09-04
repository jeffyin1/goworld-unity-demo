using System.Collections;
using System.Collections.Generic;
using GoWorldUnity3D;
using UnityEngine;
using UnityEngine.UI;

public class EntityInit
{
    public long Hp = 0;
    public string Name = "default";
}

public class EntityStatus : MonoBehaviour {

    public Text m_hpText;
    public Text m_nameText;

    EntityInit m_data = new EntityInit();

    public void init(EntityInit data)
    {
        m_data = data;
    }

    public void InitWithAttr(MapAttr attr)
    {
        m_data.Hp = attr.GetInt("hp");
        m_data.Name = attr.GetStr("name");
    }

    public void SetHp(long hp)
    {
        m_data.Hp = hp; 
    }

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        m_hpText.text = m_data.Hp.ToString();
        m_nameText.text = m_data.Name;
    }
}
