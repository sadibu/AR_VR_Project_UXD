using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCollection : MonoBehaviour
{
    // hey, this is a useless comment :D

    public List<Book> Books { get; set; } = new List<Book>();

    void Start()
    {
        GenerateBook("Lauren Graham", "Talking as fast as I can", "From Gilmore Girls to Gilmore Girls", 9.90f);
        GenerateBook("Lucinda Riley", "The Shadow Sister", "The seven sister series", 12.90f);
        GenerateBook("Patrick Rothfuss", "The Name of the Wind", "Some black guy with robe", 10.90f);
        GenerateBook("Don Norman", "The Design of Everyday Things", "UX Expert about Teapots", 8.90f);
    }

    private void GenerateBook(string v1, string v2, string v3, float v4)
    {
        Book b1 = new Book(v1, v2, v3, v4);
            b1.Ratings.Add(5);
        Books.Add(b1);
    }
}

public class Book
{
    public string Author { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public List<int> Ratings { get; set; } = new List<int>();

    public Book(string author, string title, string description, float price)
    {
        this.Author = author;
        this.Title = title;
        this.Description = description;
        this.Price = price;
    }

    public int GetStars()
    {
        if (Ratings == null) return 0;
        if (Ratings.Count == 0) return 0;

        float sum = 0;
        foreach(int number in Ratings)
        {
            sum += number;
        }
        return Mathf.RoundToInt(sum  * 1.0f / Ratings.Count);
    }
}