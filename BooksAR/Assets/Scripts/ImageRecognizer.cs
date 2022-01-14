using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageRecognizer : MonoBehaviour
{
    private ARTrackedImageManager arTrackedImageManager;
    public GameObject StarsGO;
    public GameObject BookGO;
    public GameObject DescriptionGo;
    public GameObject RatingsGo;
    public GameObject ReviewsGo;
    public GameObject SavedGo;

    public BookCollection BookCollection;

    private Book currentlyTrackedBook;
    private bool back;

    void Awake()
    {
        arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var addedImage in args.added)
        {
            UpdateImage(addedImage);
        }

        foreach (var updatedImage in args.updated)
        {
            UpdateImage(updatedImage);
        }

        foreach (var removedImage in args.removed)
        {
            UpdateImage(removedImage);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        //Tracking Book
        if (currentlyTrackedBook == null && trackedImage.trackingState == TrackingState.Tracking)
        {
            //Make Stars Visible, Book Visible and correct UI visible
            string name = trackedImage.referenceImage.name;

            //handle back of same book
            back = false;
            if (name.Contains("_BACK"))
            {
                back = true;
                name = name.Substring(0, name.Length - 5);
            }
            Vector3 position = trackedImage.transform.position;
            Book trackedBook = BookCollection.Books.Where(b => b.Title.Replace(" ", "") == name).FirstOrDefault();
            currentlyTrackedBook = trackedBook;

            //Set Stars accordingly to ratings
            SetStars(trackedBook.GetStars());

            //Set UI Visible / Not Visible
            BookGO.SetActive(true);
            if (!back)
            {
                DescriptionGo.SetActive(true);
                RatingsGo.SetActive(false);
                ReviewsGo.SetActive(false);
            }
            else
            {
                DescriptionGo.SetActive(false);
                RatingsGo.SetActive(false);
                ReviewsGo.SetActive(true);
            }
                
            //set positions
            BookGO.transform.position = position;
        }


        //Tracking Lost
        if (trackedImage.trackingState == TrackingState.Limited)
        {
            //Remove UIs, Stars and 3D Book when tracking is lost
            currentlyTrackedBook = null;
            back = false;
            DescriptionGo.SetActive(false);
            ReviewsGo.SetActive(false);
            RatingsGo.SetActive(false);
            BookGO.SetActive(false);
            foreach (Transform child in StarsGO.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    //set Stars visible according to ratings
    private void SetStars(int stars)
    {
        foreach(Transform child in StarsGO.transform)
        {
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i < stars; i++)
        {
            StarsGO.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    //gets called every frame, looks for touch input
    void Update()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && !back)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                // change ui if star is hit

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    var name = hit.collider.gameObject.name;
                    if (name.Contains("BOOK") || name.Contains("plus"))
                    {
                        SavedGo.SetActive(true);
                    }
                    else
                    {
                        DescriptionGo.SetActive(false);
                        RatingsGo.SetActive(true);
                    }
                }
            }
        }
    }
}
