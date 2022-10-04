# Unity-ProjectStructure

主要是建立一個自動建立簡易專案資料夾結構。

此參考 [UnityProjectTreeGenerator][ref] 方法建立資料夾，

## 使用方式

`Tools > Generate Project Structure`

必須要設定 Root Name 才能點擊 `Create Structure`

![img_1]

## 資料夾結構

``` text
|- Assets
    |- Project Name /// 自己設定
        |- 00_Art
        |   |- 00_Profabs
        |   |   |- Models
        |   |   |- UI
        |   |- 01_Shaders
        |   |   |- UI_Shaders
        |   |- 02_Timeline
        |   |- 03_Models
        |   |   |- Example_Model
        |   |   |   |- 3D
        |   |   |   |- Animation
        |   |   |   |- Textures
        |   |   |- Example_Effect
        |   |       |- Textures
        |   |- 04_Scenes
        |   |- 05_UI
        |   |   |- Textures
        |   |   |- Effect
        |   |       |- Textures
        |   |       |- Animation
        |   |       |- Material
        |   |- 07_Audio
        |   |- 08_Video
        |- 01_Program
           |- 00_Scenes
           |- 01_Scripts
           |- 02_Tests
           |- 03_Prefabs
           |- 05_UI 
```

## [Github][github]

____________________________________________________________

[img_1]:https://imgur.com/iBAEGNO.png
[ref]:https://github.com/dkoprowski/UnityProjectTreeGenerator

[github]:https://github.com/Wenrong274/Unity-ProjectStructure
