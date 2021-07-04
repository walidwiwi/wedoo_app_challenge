using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Article : MonoBehaviour
{
    public Text name;
    public Sprite[] images;
    public Image image_display_continer;
    private int[] isImageReadyToLoad;
    public string id;
    public int PriceIniger;
    public Text price;
    public Text remis;
    public GameObject uploading_panel;
    public string priceActualString; 
    // Start is called before the first frame update
    bool is_configured = false; 
    private void Update()
    {
        if(!is_configured)
        {
 
            StartCoroutine(start_get_info());
            
            is_configured = true;
        }
    }
    
    public GameObject left;
    public GameObject right;
    IEnumerator load()
    {
        WWWForm form = new WWWForm();
        form.AddField("article_id", id);

        WWW site = new WWW(appManager.host + "/load.php", form);
        yield return site;

        if (site.text == "" || site.text[0] == '<')
        {
            StartCoroutine(load());
            print("implicite");
            
        }else{
            string[] ress = site.text.Split('|');
            name.text = ress[0];

            PriceIniger = int.Parse(ress[2]);
            normal_price = ress[1] + "Da"; // normal prix

            // set price rage
            if (PriceIniger > PriceController.maxPriceAvariable )
                PriceController.maxPriceAvariable = PriceIniger;

            if (PriceIniger < PriceController.minPriceAvariable)
                PriceController.minPriceAvariable = PriceIniger;

            PriceController.instance.StartSliderChangeValu();
            PriceController.instance.price_slider.value = 1;

            if (price != null)
            {
                 
                price.text = ress[2];// prix
                remis.text = "-" + (100 - (int.Parse(ress[2]) / (float)int.Parse(ress[1]) * 100)).ToString("00.0") + "%";
                if (int.Parse(ress[1]) == int.Parse(ress[2])) remis.text = "";
                check_button_if_disponible();
            }
            priceActualString = PriceIniger.ToString(); 
            StartCoroutine(loadImage());
        }
    }
    public  string normal_price;
    public void check_button_if_disponible()
    {
        if (left == null) return; 
        left.SetActive(  image_index_to_load > 0 );
        right.SetActive(   image_index_to_load < isImageReadyToLoad.Length - 1); 
    }
    public void display_next_image()
    {
        if (image_index_to_load >= isImageReadyToLoad.Length - 1) return;
         
         image_index_to_load++;
        StartCoroutine(loadImage());
    }
    public void display_last_image()
    {
        if (image_index_to_load <= 0) return; 
        image_index_to_load--;
        StartCoroutine(loadImage()); 
    }
 
    IEnumerator start_get_info()
    {
        WWWForm form = new WWWForm();
        form.AddField("article_id", id);

        WWW site = new WWW(appManager.host + "/load_image_num.php", form);
        yield return site;
        int nb;
        if (int.TryParse(site.text, out nb))
        {
            isImageReadyToLoad = new int[nb];
            images = new Sprite[nb];
            StartCoroutine(load());

        }
        else
            StartCoroutine(start_get_info());

         
       
    }

    private int image_index_to_load = 0;
    IEnumerator loadImage()
    {
        uploading_panel.SetActive(true); 
        if (isImageReadyToLoad[image_index_to_load] == 1)
        {
            image_display_continer.sprite = images[image_index_to_load];
        }else
        {
            WWWForm form = new WWWForm();
            form.AddField("article_id", id);
            form.AddField("image_index", image_index_to_load+1);
            WWW site = new WWW(appManager.host + "/load_image.php" , form);
            yield return site;
            if (site.text.Length == 0)
                StartCoroutine(loadImage());
            else
            {

                string imageStringB64 = site.text;
                byte[] imageBytesData = System.Convert.FromBase64String(imageStringB64);

                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(imageBytesData);
                tex.Apply();

                images[image_index_to_load] = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));
                isImageReadyToLoad[image_index_to_load] = 1;

                image_display_continer.sprite = images[image_index_to_load];
            }
        }
        uploading_panel.SetActive(false); 

    }
    public void copierToBag()
    {

        GameObject b = Instantiate(bag_shoping.bagg.bagElement , bag_shoping.bagg.bagPlace) ;
        b.GetComponent<bag_element>().name.text = name.text;
        b.GetComponent<bag_element>().price.text = PriceIniger.ToString();
        b.GetComponent<bag_element>().PriceIniger = PriceIniger;
        if(images != null &&images.Length != 0) b.GetComponent<bag_element>().image.sprite = images[0];
        b.transform.SetSiblingIndex(0);

        int oldPrice = (int)validator.v.PriceIniger;
        int newPrice = oldPrice + PriceIniger;
        validator.v.PriceIniger = newPrice;

        //validator.v.totalPrice.text = "total :" + newPrice.ToString() + "Da";

        bag_icon_manager.manager.add();
   }
    public void onClickme()
    {
        appManager.app.showDetelsPanel();
        Invoke("conf", 0.2f);
    }
    public void conf()
    {
        Details_article_view.d.SetUp(this);
    }
}
