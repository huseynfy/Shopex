let http=new XMLHttpRequest();
http.onreadystatechange=function(){
    if(this.readyState==4 && this.status==200){
        let response=JSON.parse(this.responseText);
        for(let post of response){

            let temperature=document.getElementById("temperature");
            temperature.innerText=post.temp;

            let city=document.getElementById("cityname")
            city.innerText=post.city_name;

            let humidity=document.getElementsByClassName("goleft");
            humidity.innerText=post.humidity;

            let wind=document.getElementsByClassName("goright");
            wind.innerText=post.wind_spd;
        }
    }
}
http.open("GET","http://api.weatherbit.io/v2.0/current?city=Baku&country=Azerbaijan&key=fee4ee70c23d42008adc385c3e30d771");
http.send();