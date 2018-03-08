$(document).ready(function(){
    $.ajax({
            "url": "http://Triplann.com:5000/api/",
            "method": "GET",
        }).then(d => (
            console.log(d)
        ));
    })
    