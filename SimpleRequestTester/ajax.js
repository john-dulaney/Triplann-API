$(document).ready(function(){
    $.ajax({
            "url": "http://localhost:5000/api/ChecklistItem",
            "method": "GET",
        }).then(d => (
            console.log(d)
        ));
    })