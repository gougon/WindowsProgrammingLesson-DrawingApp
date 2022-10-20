# WindowsProgrammingLesson-DrawingApp
**說明**<br>
實作一個簡易的繪圖軟體(Windows Form / Windows App)

**目的**<br>
練習Design Pattern

**主要功能**<br>
* 可以繪畫直線、矩形、六邊形
* 可以一鍵清除畫面上所有的圖案
* Redo, Undo
* 選擇圖案後可變形
* 藉由google drive api，可以save目前的畫布，並且可以在之後load回來

**Design Pattern**<br>
* **MVP pattern**
  * View, PresentationModel, Model
* **Adapter pattern**
  * 由於有Form及App，各自的圖形介面不同，因此使用Adapter pattern共用model
* **Observer pattern**
  * 由於是MVC架構，當model的圖形改變時，需要藉由Observer pattern通知view做改變
* **Factory pattern**
  * 使用simple factory來產生三種不同的形狀
* **Command pattern**
  * redo, undo, clear, resize皆是藉由Command pattern實現，將命令與實際的物件分隔，避免bad smell
* **State pattern**
  * 由於繪畫程式具有pointer, drawing這2種不同的狀態，因此使用State pattern避免程式碼過時複雜

  
**DEMO**<br>
https://www.youtube.com/watch?v=081FqAVnTC8
