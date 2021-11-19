//notification
setTimeout(function () {
    $('#err1').fadeOut('slow');
}, 3000);

//open modal
$('#sendinfo').on('click', function () {
    $('#info').modal();
})