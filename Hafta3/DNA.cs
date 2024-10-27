using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    int gene;
    int maxValues;

    public DNA(int max){
        maxValues = max;
        
        gene= (Random.Range(0,maxValues)); // 0 is included and maxValues is excluded
        
    }

    public void Combine( DNA dna1, DNA dna2){// gets gene from both parents and combines it 
        
            if (Random.Range(0,100)>=50)
            {
                gene= dna1.gene;
            }
            else{
                gene= dna2.gene;
            }
    }
    public void Mutate(){
        gene= Random.Range(0,maxValues);
    }


    public void SetGene( int value){
        gene=value;
    }

    public int getGene(){
        return gene;
    }
}
