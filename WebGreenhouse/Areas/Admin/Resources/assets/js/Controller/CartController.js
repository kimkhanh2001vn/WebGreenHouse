var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnRemoveAll').off('click').on('click', function () {
            $.ajax({
                url: 'Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart";
                    }
                }
            })
        });

        $('.btn-delete').off('click').on('click', function () {
            $.ajax({
                data: { id: $(this).data('id') },
                url: 'Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Cart";
                    }
                }
            });
        });

        //truyen vao e e.preventDefault xu ly # cua the a
    }
}
cart.init();