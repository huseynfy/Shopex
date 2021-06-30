//jQuery 
$(document).ready(function(){
    $(".titleofcollapsible").click(function (){
        if ($(this).next()[0] != $(".active1")[0]){
            $(".active1").slideUp("slow", function() {
                $(this).removeClass("active1")
            })
            $(this).next().slideDown("slow",function(){
                $(this).addClass("active1")
            })
        }else{
            $(".active1").slideUp("slow",function(){
                $(this).removeClass("active1")
            })
        }
    })
    //Toggle
    $(".burgermenu").click(function(){
        $(".rownavbar").toggle("d-block")
    })
    $(".btncart").click(function(){
        $(".dropdowncartcontent").toggle("d-none")
    })
    $(".btnshop").click(function(){
        $(".dropdown-shop-content").toggle("d-none")
    })
    $(".btnpages").click(function(){
        $(".dropdown-pages-content").toggle("d-none")
    })
    $(".btncurrency").click(function(){
        $(".dropdown-currency-content").toggle("d-none")
    })
    $(".btnlanguage").click(function(){
        $(".dropdown-languages-content").toggle("d-none")
    })
    //Toggle end
})
//End

//Script for tabs

let menus = document.querySelectorAll(".menu div");
let contents = document.querySelectorAll(".contentoftab div");
for (let menu of menus) {
    menu.addEventListener("click", function () {
        let active = document.querySelector(".activetab");
        active.classList.remove("activetab");
        this.classList.add("activetab");

        let index = this.getAttribute("data-index");
        for (let content of contents) {
            if (content.getAttribute("data-index") == index) {
                content.classList.remove("d-none")
            } else {
                content.classList.add("d-none")
            }
        }
    })
}
//End

//Script for Products filter index.html
let filters=document.querySelectorAll(".productsfilter span")
let filterrows=document.querySelectorAll(".productrow")

for(let filter of filters){
    filter.addEventListener("click",function(){
        let active=document.querySelector(".activefilter")
        active.classList.remove("activefilter")
        this.classList.add("activefilter")

        let index=this.getAttribute("data-index")
        for(let filterrow of filterrows){
            if(filterrow.getAttribute("data-index")==index){
                filterrow.classList.remove("d-none")
            }else{
                filterrow.classList.add("d-none")
            }
        }
    })
}
//End


 //Script of quantity increase/decrease
function increaseValue() {
    var value = parseInt(document.getElementById('number').value, 10);
    value = isNaN(value) ? 0 : value;
    value++;
    document.getElementById('number').value = value;
  }
  
  function decreaseValue() {
    var value = parseInt(document.getElementById('number').value, 10);
    value = isNaN(value) ? 0 : value;
    value < 1 ? value = 1 : '';
    value--;
    document.getElementById('number').value = value;
  }
  //End

  // Script for Dark Mode
  function myFunction() {
    var element = document.body;
    element.classList.toggle("darkbody");
 }
 //End

  //Slider index.html start
   let max=$(".itemsliderfirstpage").length;
   $(".imagessliderfirstpage").width(max*100+"%");
   $(".itemsliderfirstpage").width(100/max+"%");
   let count = 0;
 
   $(".rightmain").click(rightClick);
 
   function rightClick(){
       if(count<max-1){
           count++;
           $(".imagessliderfirstpage").animate({
               "margin-left": "-" + 100 * count + "%"
           })   
       }else{
           $(".imagessliderfirstpage").animate({
           "margin-left": "0"
       })
       count=0;
       }
   }
   $(".leftmain").click(function () {
       if (count > 0) {
           count--;
           $(".imagessliderfirstpage").animate({
               "margin-left": "-" + 100 * count + "%"
           })  
       }else{
           $(".imagessliderfirstpage").animate({
               "margin-left": "-" + 100 * (max-1) + "%"
           })
           count=max-1;
       }
   })
   let slidefirstpage=setInterval(rightClick,3000);
 
   $(".leftmain, .rightmain").mouseenter(function(){
       clearInterval(slidefirstpage)
   })
   $(".leftmain, .rightmain").mouseleave(function(){
       slidefirstpage=setInterval(rightClick,3000);
   })
   //Slider End