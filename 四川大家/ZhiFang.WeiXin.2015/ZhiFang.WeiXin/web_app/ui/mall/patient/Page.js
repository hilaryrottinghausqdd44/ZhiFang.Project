/**医生页面公共JS*/
$(function () {
    //返回按钮监听
    $(".page-top-back").on("click", function () {
        history.go(-1);
    });
    //返回主页监听
    $(".page-top-home").on("click", function () {
        location.href = JcallShell.System.Path.UI + '/mall/patient/home/home.html';
    });
});