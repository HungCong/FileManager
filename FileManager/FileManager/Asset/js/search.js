$(document).ready(function () {
    $('#Keyword').autocomplete({
        minLength: 0,
        source: function (request, response) {
            $.ajax({
                url: "/Home/SearchFile",//Link lấy dữ liệu gợi ý
                type: "POST",
                dataType: "json",
                data: {
                    q: request.term
                },
                success: function (res) {
                    response(res.data);
                }
            });
        },
        focus: function (event, ui) {
            $('#Keyword').val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $('#Keyword').val(ui.item.label);
            //$("#project-id").val(ui.item.value);
            //$("#project-description").html(ui.item.desc);
            //$("#project-icon").attr("src", "images/" + ui.item.icon);

            return false;
        }
    })
     .autocomplete("instance")._renderItem = function (ul, item) {
         return $("<li>")
           .append("<div>" + item.label + "</div>")
           .appendTo(ul);
     };
});
        
   

//$('#Keyword').autocomplete({
//    minLength: 0,
//    source: function (request, response) {
//        $.ajax({
//            url: "/Home/SearchFile",//Link lấy dữ liệu gợi ý
//            dataType: "json",
//            data: {
//                q: request.term
//            },
//            success: function (res) {
//                response(res.data);
//            }
//        });
//    },
//    focus: function (event, ui) {
//        $('#Keyword').val(ui.item.label);
//        return false;
//    },
//    select: function (event, ui) {
//        $('#Keyword').val(ui.item.label);
//        //$("#project-id").val(ui.item.value);
//        //$("#project-description").html(ui.item.desc);
//        //$("#project-icon").attr("src", "images/" + ui.item.icon);

//        return false;
//    }
//})
//     .autocomplete("instance")._renderItem = function (ul, item) {
//         return $("<li>")
//           .append("<div>" + item.label + "<br>" + item.desc + "</div>")
//           .appendTo(ul);
//     };