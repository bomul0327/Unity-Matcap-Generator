# Unity-Matcap-Generator

## Overview

 This is a simple matcap texture generator for Unity users. You can check a generated matcap texture in realtime. Also, you can save the texture if you want. This one can generate various types of material, if you can handle unity's rendering system, such as lights, materials, shaders or something else.

 The detailed documentation will be added at Wiki soon.

## Checked Unity version
* 2018.4.14
* 2019.2.16f1

 I didn't check URP or HDRP version. It may works but you need to modify a material, because the Standard shader in Unity is not supported in URP or HDRP.
 
## Usage
### Download
 You can download this from the github repository or from releases(https://github.com/bomul0327/Unity-Matcap-Generator/releases)
 
### Install
 Please install through package, not source code.

### How to Use?
 1. Place "Matcap Generator" prefab on your scene.
 2. Select the prefab.
 3. By changing variables, you can change the size of texture, path.
 4. If you want to apply your matcap texture to your material, add the material on "Target Material". Then, Check "Apply Matcap Texture".
 5. By changing "Target Texture Property", you can apply matcap texture where you want.
 6. Press "Save Matcap Texture" to save your matcap texture.

## License
 You can use this for free.
