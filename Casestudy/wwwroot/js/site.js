// Write your JavaScript code.
$(function () {
    if ($("#register_popup") !== undefined) {
        $('#register_popup').modal('show');
    }
    /// login valid popup
    if ($("#login_popup") !== undefined) {
        $('#login_popup').modal('show');
    }

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
    $('.nav-tabs a').on('show.bs.tab', function (e) {
        if ($(e.relatedTarget).text() === 'Demographic') { // tab 1
            $('#Firstname').valid();
            $('#Lastname').valid();
            $('#Age').valid();
            $('#CreditcardType').valid();
            if ($('#Firstname').valid() === false || $('#Lastname').valid() === false || $('#Age').valid() === false || $('#CreditcardType').valid() === false) {
                return false; // suppress click
            }
        }
        if ($(e.relatedTarget).text() === 'Address') { // tab 2
            $('#Address1').valid();
            $('#City').valid();
            $('#Region').valid();
            $('#Country').valid();
            $('#Mailcode').valid();
            if ($('#Address1').valid() === false || $('#City').valid() === false) {
                return false; // suppress click
            }
        }
        if ($(e.relatedTarget).text() === 'Account') { // tab 3
            $('#Email').valid();
            $('#Password').valid();
            $('#RepeatPassword').valid();
            if ($('#Email').valid() === false || $('#Password').valid() === false || $('#RepeatPassword').valid() === false) {
                return false; // suppress click
            }
        }
    }); // show bootstrap tab
});
let CopyToModal = (id, data) => {
    $("#qty").val("0");
    $("#price").text(data.Price);

    $("#description").text(data.Description);
    $("#detailsGraphic").attr("src", data.GraphicName);
    $("#detailsId").val(id);
};