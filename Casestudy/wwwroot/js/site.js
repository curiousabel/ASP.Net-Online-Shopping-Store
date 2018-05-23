// Write your JavaScript code.
$(function () {
    // details click - to load popup on catalogue
    // display message if modal still loaded
    if ($('#detailsId').val() > 0) {
        var data = $('#modalbtn' + $('#detailsId').val()).data('details');
        CopyToModal($('#detailsId').val(), data);
        $('#details_popup').modal('show');
    }
    $("a.btn-default").on("click", (e) => {
        let Id = e.target.dataset.id;
        let data = JSON.parse(e.target.dataset.details); // it's a string need an object
        $("#results").text("");
        CopyToModal(Id, data);
    });
});
let CopyToModal = (id, data) => {
    $("#qty").val("0");
    $("#price").text(data.Price);
   
    $("#description").text(data.Description);
    $("#detailsGraphic").attr("src", data.GraphicName);
    $("#detailsId").val(id);
}