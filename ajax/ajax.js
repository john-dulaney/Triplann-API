$(document).ready(function(){
    $.ajax({
            "url": "http://bangazon.com:5000/api/Department/1",
            "method": "GET",
        }).then(d => (
            console.log(d)
        ));
    })
    