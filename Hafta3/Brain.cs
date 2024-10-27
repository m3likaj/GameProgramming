using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Brain : MonoBehaviour
{
    public float timeAlive;
    public DNA dna;
    private ThirdPersonCharacter m_character;
    private Vector3 m_move;
    private bool m_jump = false;
    public bool isAlive = true;
    public float distanceTravelled=0;
    Vector3 startPosition;



    void OnCollisionEnter(Collision other){
        
        if (other.gameObject.tag == "dead")
        {
            isAlive =false;
            
        }

    }

    public void Init(){
        dna = new DNA(6);
        m_character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        isAlive = true;
        startPosition = this.transform.position;
    }


    // Update is called in a fixed timeframe, can be called more than once per frame
    void FixedUpdate()
    {
         // 0->forward, 1->back, 2->left, 3->right, 4->jump, 5->crouch
        float horizontal=0;
        float vertical =0;
        bool crouch = false;

        if(dna.getGene()==0){
            vertical = 1;
        }
        else if(dna.getGene()==1){
            vertical = -1;
        }
         else if(dna.getGene()==2){
            horizontal = -1;
        }
         else if(dna.getGene()==3){
            horizontal = 1;
        }
         else if(dna.getGene()==4){
            m_jump = true;
         }
         else if(dna.getGene()==5){
            crouch = true;
        }
        m_move = Vector3.forward * vertical + Vector3.right * horizontal;
        m_character.Move(m_move, crouch, m_jump);
        m_jump= false;
        if (isAlive){
            timeAlive+= Time.deltaTime;
            distanceTravelled = Vector3.Distance(this.transform.position, startPosition);
        }


    }
}
