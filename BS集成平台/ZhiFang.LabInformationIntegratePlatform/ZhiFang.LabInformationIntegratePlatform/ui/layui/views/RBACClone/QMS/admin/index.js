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
    element.on('tab(qms)', function (data) {
        //console.log(data);
    });
    $("#showhrdeptbutton").on("click", function () { FetchingData("hrdept");  });
    $("#stnchrdeptbutton").on("click", function () { FetchingStncData("hrdept"); });
    $("#showhremployeebutton").on("click", function () { FetchingData("hremployee");});
    $("#stnchremployeebutton").on("click", function () { FetchingStncData("hremployee"); });
    $("#showhrpositionbutton").on("click", function () { FetchingData("hrposition");});
    $("#stnchrpositionbutton").on("click", function () { FetchingStncData("hrposition"); });
    $("#showhrempIdentitybutton").on("click", function () { FetchingData("hrempIdentity");});    
    $("#stnchrempIdentitybutton").on("click", function () { FetchingStncData("hrempIdentity");});
	$("#showhrdeptEmpbutton").on("click", function () { FetchingData("hrdeptEmp");});
    $("#stnchrdeptEmpbutton").on("click", function () { FetchingStncData("hrdeptEmp");});
    $("#showhrdeptIdentitybutton").on("click", function () { FetchingData("hrdeptIdentity");});
    $("#stnchrdeptIdentitybutton").on("click", function () { FetchingStncData("hrdeptIdentity");});
    $("#showrbacuserbutton").on("click", function () { FetchingData("rbacuser");});
    $("#stncrbacuserbutton").on("click", function () { FetchingStncData("rbacuser");});
    $("#showrbacrowFilterbutton").on("click", function () { FetchingData("rbacrowFilter");});
    $("#stncrbacrowFilterbutton").on("click", function () { FetchingStncData("rbacrowFilter");});
    $("#showrbacrolebutton").on("click", function () { FetchingData("rbacrole");});
    $("#stncrbacrolebutton").on("click", function () { FetchingStncData("rbacrole");});
    $("#showrbacmodulebutton").on("click", function () { FetchingData("rbacmodule");});
    $("#stncrbacmodulebutton").on("click", function () { FetchingStncData("rbacmodule");});
    $("#showrbacroleModulebutton").on("click", function () { FetchingData("rbacroleModule");});
    $("#stncrbacroleModulebutton").on("click", function () { FetchingStncData("rbacroleModule");});
    $("#showrbacmoduleOperbutton").on("click", function () { FetchingData("rbacmoduleOper");});
    $("#stncrbacmoduleOperbutton").on("click", function () { FetchingStncData("rbacmoduleOper");});
    $("#showrbacroleRightbutton").on("click", function () { FetchingData("rbacroleRight");});
    $("#stncrbacroleRightbutton").on("click", function () { FetchingStncData("rbacroleRight");});
    $("#showrbacempRolesbutton").on("click", function () { FetchingData("rbacempRoles");});
    $("#stncrbacempRolesbutton").on("click", function () { FetchingStncData("rbacempRoles");});
    $("#showrbacempOptionsbutton").on("click", function () { FetchingData("rbacempOptions");});
    $("#stncrbacempOptionsbutton").on("click", function () { FetchingStncData("rbacempOptions");});


    function FetchingData(type) {
        var me = this;
        switch (type) {
            case "hrdept":
                $("#hrdeptiframe").css("display", "inline-block");
                break;
            case "hremployee":
                $("#hremployeeiframe").css("display", "inline-block");
                break;
            case "hrposition":
                $("#hrpositioniframe").css("display", "inline-block");
                break;
            case "hrempIdentity":
                $("#hrempIdentityiframe").css("display", "inline-block");
                break;
            case "hrdeptEmp":
                $("#hrdeptEmpiframe").css("display", "inline-block");
                break;
            case "hrdeptIdentity":
                $("#hrdeptIdentityiframe").css("display", "inline-block");
                break;
            case "rbacuser":
                $("#rbacuseriframe").css("display", "inline-block");
                break;
            case "rbacrowFilter":
                $("#rbacrowFilteriframe").css("display", "inline-block");
                break;
            case "rbacrole":
                $("#rbacroleiframe").css("display", "inline-block");
                break;
            case "rbacmodule":
                $("#rbacmoduleiframe").css("display", "inline-block");
                break;
            case "rbacroleModule":
                $("#rbacroleModuleiframe").css("display", "inline-block");
                break;
            case "rbacmoduleOper":
                $("#rbacmoduleOperiframe").css("display", "inline-block");
                break;
            case "rbacroleRight":
                $("#rbacroleRightiframe").css("display", "inline-block");
                break;
            case "rbacempRoles":
                $("#rbacempRolesiframe").css("display", "inline-block");
                break;
            case "rbacempOptions":
                $("#rbacempOptionsiframe").css("display", "inline-block");
                break;
        } 
    };
    function FetchingStncData(type) {
        var me = this;
        var url = "";
        switch (type) {
            case "hrdept":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchHRDeptByQMS';
                 break;
            case "hremployee":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchHREmployeeByQMS';
                break;
            case "hrposition":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchHRPositionByQMS';
                break;
            case "hrempIdentity":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchHREmpIdentityByQMS';
                break;
            case "hrdeptEmp":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchHRDeptEmpByQMS';
                break;
            case "hrdeptIdentity":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchHRDeptIdentityByQMS';
                break;
            case "rbacuser":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACUserByQMS';
                break;
            case "rbacrowFilter":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACRowFilterByQMS';
                break;
            case "rbacrole":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACRoleByQMS';
                break;
            case "rbacmodule":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACModuleByQMS';
                break;
            case "rbacroleModule":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACRoleModuleByQMS';
                break;
            case "rbacmoduleOper":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACModuleOperByQMS';
                break;
            case "rbacroleRight":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACRoleRightByQMS';
                break;
            case "rbacempRoles":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACEmpRolesByQMS';
                break;
            case "rbacempOptions":
                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchRBACEmpOptionsByQMS';
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
	