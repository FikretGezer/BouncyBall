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
 * Renk paneli hep açýk dursun
 * Sadece seçilen rengin üzerine o rengin þuan seçili olduðuna dair bir ibare belirsin.
 * Kilitli olanlar soluk renkte olsun.
 * Seçili olmayanlar görünür renkte olsun.
 * Seçilecek renkler olan color1,color2,color3 scriptableobject olsun. Böylece ekstra uðraþmadan seçilen renk direkt kaydedilebilir.
 * Seçilecek renge týklanýldýðý zaman (örnek color1'e) aktif olarak bulunan renk "#" yada farklý bir iþaret sadece seçili rengi belirtmek için. 
 * Eðer diðer 2'renkten birini seçerse yani color1'ken color2 veya color3'unkunu seçerse o renkler null veya siyah gözüksün ve onlar seçilmeden menuden çýkýþ yapýlmasýn ya da random renk atasýn. 
 * Ayrýca save butonu eklensin ve butona týklanmadan kaydedilmesin. 
 * 
 * 
 * 
 * butona týkla btn id kaydet
 * 
 * 
 * 
 * 
 */
