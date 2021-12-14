/**
 * 检验之星6.6组织机构同步
 * @author guohaixiang
 * @version 2020-03-18
 */
layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'element','layer'], function () {
    var layer = layui.layer,
        uxutil = layui.uxutil,
        $ = layui.jquery,
        element = layui.element;
    element.on('tab(jinayan6.6)', function (data) {
        //console.log(data);
    });
    $("#showdeptbutton").on("click", function () { FetchingData("dept");  });
    $("#stncdeptbutton").on("click", function () { FetchingStncData("dept"); });
    $("#showdoctorbutton").on("click", function () { FetchingData("doctor");});
    $("#stncdoctorbutton").on("click", function () { FetchingStncData("doctor"); });
    $("#shownpuserbutton").on("click", function () { FetchingData("npuser");});
    $("#stncnpuserbutton").on("click", function () { FetchingStncData("npuser"); });
    $("#showpuserbutton").on("click", function () { FetchingData("puser");});
    $("#stncpuserbutton").on("click", function () { FetchingStncData("puser");});



    function FetchingData(type) {
        var me = this;
        switch (type) {
            case "dept":
                $("#deptiframe").css("display", "inline-block");
                break;
            case "doctor":
                $("#doctoriframe").css("display", "inline-block");
                break;
            case "npuser":
                $("#npuseriframe").css("display", "inline-block");
                break;
            case "puser":
                $("#puseriframe").css("display", "inline-block");
                break;
        } 
    };
    function FetchingStncData(type) {
        var me = this;
        var url = "";
        switch (type) {
            case "dept":
                 url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchDeptByLabStar6';
                 break;
            case "doctor":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchDoctorByLabStar6';
                 break;
            case "npuser":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchNPUserByLabStar6';
                 break;
            case "puser":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchPUserByLabStar6';
                 break;
        }
        uxutil.server.ajax({
            url: url
        }, function (data) {                
                if (data) {
                    if (data.success) {
                        layer.msg('同步成功！');
                    } else {
                        layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
                    }
                } else {
                    layer.msg('同步异常！');
                }
        });
    };
});
	