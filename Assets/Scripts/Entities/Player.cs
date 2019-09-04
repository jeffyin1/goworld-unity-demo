using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoWorldUnity3D;
using System.Threading;

public class Player : GameEntity {

	Animator anim;
	PlayerShooting playerShooting;
    EntityStatus m_status;

    public void Init( EntityStatus status)
    {
        m_status = status;
    }

    protected override void OnCreated() {
        GameObject.DontDestroyOnLoad(this.gameObject);

        anim = gameObject.GetComponent<Animator> ();
		playerShooting = gameObject.GetComponent<PlayerShooting> ();
		Debug.Log ("Player is created");
	}

    protected override void OnDestroy() {
		Debug.Log ("Player is destroyed");
	}

	protected override void OnEnterSpace() {
		if (this.IsClientOwner)
        {
            SceneManager.LoadScene("Level 01", LoadSceneMode.Single);
            // UnityEngine.SceneManagement.SceneManager.LoadScene("Level 01", LoadSceneMode.Single);
        }
        gameObject.GetComponent<PlayerMovement> ().enabled = this.IsClientOwner;

		string action = this.Attrs.GetStr("action");
        anim.SetTrigger (action);

        long hp = this.Attrs.GetInt("hp");
        m_status.SetHp(hp);
        m_status.InitWithAttr(this.Attrs);
    }

	public void OnAttrChange_action() {
        string action = this.Attrs.GetStr("action");
		Debug.Log (this.ToString() + "'s action is changed to " + action); 
		anim.SetTrigger (action);
	}
    public void OnAttrChange_hp()
    {
        long hp = this.Attrs.GetInt("hp");
        Debug.Log(this.ToString() + "'hp is changed to " + hp.ToString());
        m_status.SetHp(hp);
    }


    public void Shoot() {
		playerShooting.Shoot ();
	}

    protected override void OnBecomeClientOwner()
    {

    }

    protected override void OnLeaveSpace()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        // GoWorldUnity3D.Logger.Debug("Player", "Player Update. ..");

        if (this.IsClientOwner)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "Level 01")
            {
                GameObject camera = GameObject.Find("Main Camera");
                camera.GetComponent<CameraFollow>().target = gameObject.transform;
            }
        }

        if(m_status != null)
        {
            Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
            m_status.transform.position = namePos;
        }
    }

    public static new GameObject CreateGameObject(MapAttr attrs)
    {
        var worldController = GameObject.Find("GoWorldController");
        var world = worldController.GetComponent<GoWorldController>();
        var p = GameObject.Instantiate(world.Player);
        var entityStatus = world.GetEntityStatus();
        p.GetComponent<Player>().Init(entityStatus);
        return p;
    }
}
