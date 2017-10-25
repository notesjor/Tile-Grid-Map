# Tile Grid Map JSON File + XAML WorldMap

## XAML WorldMap
Based on the work of Maarten Lambrechts (https://twitter.com/maartenzam) and Mustafa Saifee (https://twitter.com/mustafasaifee42), I will share another version of the TileGridMap, implemented in WPF XAML. 

You only need two classes (both can be found under: Tile-Grid-Map/TileGridMapWpf/CorpusExplorer.Sdk.Extern.TileGridMap/):

1. EasyCountryCodeMapper.cs
Allows fast mapping of city- or country-lists - stored in a CSV file. The first column must contain the (unique) city/country-name the second column must contain the alpha-3 code (all operations in this visualization use alpha-3).

2. TileGridMap.xaml
Contains the visualization and some very helpful functions. For example: you can manipulate the border and/or background of each tile. Automatic insertion of Alpha-2, Alpha-3 and country names is also possible. The FixInnerBorder()-function eliminates unattractive boundary lines between two territories.

### TileGridMapTest
An example project, with some basic settings.

### TileGridMapJsonToXamlConverter
Converts a JSON file to a XAML-Snippet. If it's necessary to provide another map.

### Notes:
- To create CorpusExplorer.SDK.External.TileGridMap you need to create a strong name certificate.
- You should also copy the GridTileMap.json to the project output.
- XAML provides multiple control nestings, so you can show a control/chart over the county.

## Tile Grid Map JSON File
JSON file with the coordinate for world tile grid map. This JSON file is generated using the [Tile Grid Map arrangement](https://policyviz.com/2017/10/12/the-world-tile-grid-map/) by [Jon Schwabish](https://twitter.com/jschwabish) and the [ISO-3166-Countries-with-Regional-Codes database](https://github.com/lukes/ISO-3166-Countries-with-Regional-Codes) by [Luke Duncalfe](https://github.com/lukes)**(There are a few of differences between Jon's Tile grid map arrangement and the json file: India; Sri Lanka and Maldives are moved one unit up)**.

I used the excel file provided by Jon [here](http://policyviz.com/wp-content/uploads/2017/10/PolicyViz_WorldTGM.xlsx); and converted it to csv and then used a python script to combine that with the country list.

### What does the data look like?
**Tile-Grid-Map-Full.json** - It contains all the countries (some without the coordinates).

**Tile-Grid-Map-Cleaned.json** - Contains only the countries which were mentioned Tile Grid Map arrangment by Jon.

**The coordinates start from 0,0 and not from 1,1**

A single data object looks like

    "name": "Canada",
    
    "alpha-2": "CA",
    
    "alpha-3": "CAN",
    
    "country-code": "124",
    
    "iso_3166-2": "ISO 3166-2:CA",
    
    "region": "Americas",
    
    "sub-region": "Northern America",
   
    "region-code": "019",
    
    "sub-region-code": "021",
    
    "coordinates": [1,1]

### Missing Countries or Territories
* Åland Islands
* American Samoa
* Andorra
* Anguilla
* Aruba
* Bermuda
* Bonaire, Sint Eustatius and Saba
* Bouvet Island
* British Indian Ocean Territory
* Cayman Islands
* Christmas Island
* Cocos (Keeling) Islands
* Cook Islands
* Curaçao
* Falkland Islands (Malvinas)
* Faroe Islands
* French Guiana
* French Polynesia
* French Southern Territories
* Gibraltar
* Guadeloupe
* Guam
* Guernsey
* Heard Island and McDonald Islands
* Holy See
* Hong Kong
* Isle of Man
* Jersey
* Liechtenstein
* Macao
* Martinique
* Mayotte
* Monaco
* Montserrat
* New Caledonia
* Niue
* Norfolk Island
* Northern Mariana Islands
* Palestine, State of
* Pitcairn
* Puerto Rico
* Réunion
* Saint Barthélemy
* Saint Helena, Ascension and Tristan da Cunha
* Saint Martin (French part)
* Saint Pierre and Miquelon
* San Marino
* Sint Maarten (Dutch part)
* South Georgia and the South Sandwich Islands
* Svalbard and Jan Mayen
* Taiwan, Province of China
* Tokelau
* Turks and Caicos Islands
* United States Minor Outlying Islands
* Virgin Islands (British)
* Virgin Islands (U.S.)
* Wallis and Futuna
* Western Sahara

# License
![license](https://i.creativecommons.org/l/by-sa/4.0/88x31.png)  
This work is licensed under a [Creative Commons Attribution-ShareAlike 4.0 International License](https://creativecommons.org/licenses/by-sa/4.0/).
