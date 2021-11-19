var profile = {
    init: function () {
        profile.regEvents();
    },
    regEvents: function () {
        $(document).ready(function () {
            $('#btn-update').click(function () {
                $("#CustomerName").removeAttr("readonly");
                $("#username").removeAttr("readonly");
                $("#CustomerEmail").removeAttr("readonly");
                $("#CustomerWork").removeAttr("readonly");
                $("#NumberPhone").removeAttr("readonly");
                $("#CustomerAddress").removeAttr("readonly");
                $("#Description").removeAttr("readonly");
                $("#Facebook").removeAttr("readonly");
                $("#zalo").removeAttr("readonly");

                document.getElementById('save').style.visibility = 'visible';
            });
        });
    }
}
profile.init();