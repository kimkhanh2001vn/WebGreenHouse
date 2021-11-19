var registration = {
    init: function () {
        registration.regEvents();
    },
    regEvents: function () {
        $('#btnregistration').off('click').on('click', function () {
            var registrationList = [];
            registrationList.push({
                UserName: $('#UserNameRes').val(),
                CustomerName: $('#CustomerName').val(),
                CustomerAddress: $('#CustomerAddress').val(),
                CustomerWork: $('#CustomerWork').val(),
                Status: true,
                CustomerEmail: $('#CustomerEmail').val(),
                NumberPhone: $('#numberphone').val(),
                Password: $('#PassWord').val(),
                ConfirmPassWord: $('#ConfirmPassWord').val()
            });
            console.log(registrationList);
            $.ajax({
                url: "/Login/registration",
                data: {
                    RegistrationModel: JSON.stringify(registrationList)
                },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status === false) {
                        setTimeout(function () {
                            $('.noticafition').html('<p class="btn btn-outline-danger"><span>Tài Khoản Đã Tồn Tại hoặc bạn chưa điền đủ thông tin.</span></p>').fadeOut(6000);
                        }, 2000);
                    }
                    else if (res.confim === false) {
                        setTimeout(function () {
                            $('.confin').html('<p class="btn btn-outline-danger"><span>Mật khẩu không trùng khớp.</span></p>').fadeOut(6000);
                        }, 2000);
                    }
                    else if (res.empty === false) {
                        setTimeout(function () {
                            $('.empty').html('<p class="btn btn-outline-danger"><span>Không được để trống thông tin.</span></p>').fadeOut(6000);
                        }, 2000);
                    }
                    else {
                        setTimeout(function () {
                            $('.success').html('<p class="badge badge-danger"><span>Đăng ký tài khoản thành công! Mời đăng nhập.</span></p>').fadeOut(6000);
                        }, 2000);
                        location.reload();
                    }
                }
            });
        });
    }
}
registration.init();