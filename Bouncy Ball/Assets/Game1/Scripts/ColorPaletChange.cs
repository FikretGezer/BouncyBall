using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPaletChange : MonoBehaviour
{
    [SerializeField] Transform _colorsParent;
    [SerializeField] Button[] _mainColors;
    [SerializeField] ColorScriptable[] _mainColorsScriptable;

    List<Button> colorfulButtons = new List<Button>();
    int clickedCount;
    Image image;
    private void Awake()
    {
        foreach (Transform item in _colorsParent)
        {
            colorfulButtons.Add(item.GetComponent<Button>());
        }
        foreach (var item in colorfulButtons)
        {
            item.onClick.AddListener(delegate { ColorfulButtonClicked(item.GetComponent<Image>()); });
        }
        int i = 0;
        foreach (var item in _mainColors)
        {
            item.GetComponent<Image>().color = _mainColorsScriptable[i].color;
            item.onClick.AddListener(delegate { Clicked(item.GetComponent<Image>()); });
            i++;
        }
    }
    Color clr1;
    Image colorfulButtonImage;
    Image oldImage;
    Dictionary<Image, Image> chosenColors = new Dictionary<Image, Image>();
    void ColorfulButtonClicked(Image _colorfulImage)
    {
        if(clickedCount==1)
        {
            if (!chosenColors.ContainsKey(image))
                chosenColors.Add(image, oldImage);
            else
            {
                chosenColors[image].color = new Color(chosenColors[image].color.r, chosenColors[image].color.g, chosenColors[image].color.b, 1f);
                chosenColors[image] = _colorfulImage;
            }
            oldImage = _colorfulImage;
            clr1 = _colorfulImage.color;          
            _colorfulImage.color = new Color(_colorfulImage.color.r, _colorfulImage.color.g, _colorfulImage.color.b, .3f);
            _colorfulImage.GetComponent<Button>().enabled = false;
            image.color = clr1;

           
        }
    }
    void Clicked(Image _mainImage)
    {
        if(_mainImage != colorfulButtonImage)
        {
            if (clickedCount < 1)
                clickedCount++;
        }
        else
            clickedCount = 0;        

        image = _mainImage;
        colorfulButtonImage = _mainImage;
    }
}
/*
 * Renk Menusu Butonu Tasarla
 * Renk Menusu Tasarla
 * Renk Menusune Gir
 * Renk paneli hep a��k dursun
 * Sadece se�ilen rengin �zerine o rengin �uan se�ili oldu�una dair bir ibare belirsin.
 * Kilitli olanlar soluk renkte olsun.
 * Se�ili olmayanlar g�r�n�r renkte olsun.
 * Se�ilecek renkler olan color1,color2,color3 scriptableobject olsun. B�ylece ekstra u�ra�madan se�ilen renk direkt kaydedilebilir.
 * Se�ilecek renge t�klan�ld��� zaman (�rnek color1'e) aktif olarak bulunan renk "#" yada farkl� bir i�aret sadece se�ili rengi belirtmek i�in. 
 * E�er di�er 2'renkten birini se�erse yani color1'ken color2 veya color3'unkunu se�erse o renkler null veya siyah g�z�ks�n ve onlar se�ilmeden menuden ��k�� yap�lmas�n ya da random renk atas�n. 
 * Ayr�ca save butonu eklensin ve butona t�klanmadan kaydedilmesin. 
 * 
 * 
 * 
 * butona t�kla btn id kaydet
 * 
 * 
 * 
 * 
 */
