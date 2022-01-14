using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMonobehaviour : MonoBehaviour
{
    public BookCollection BookCollection;

    public Book Book { get; set; }
    
    void Start()
    {
        Book = BookCollection.Books[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Book.Title + " " + Book.Author);
    }
}
