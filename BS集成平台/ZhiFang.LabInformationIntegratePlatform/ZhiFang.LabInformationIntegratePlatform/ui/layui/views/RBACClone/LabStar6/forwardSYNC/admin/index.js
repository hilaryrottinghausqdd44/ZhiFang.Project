/**
 * 检验之星6.6组织机构同步
 * @author guohaixiang
 * @version 2020-03-18
 */
layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil','form', 'element','layer'], function () {
    var layer = layui.layer,
        uxutil = layui.uxutil,
        $ = layui.jquery,
        form = layui.form,
        element = layui.element;
    element.on('tab(jinayan6.6)', function (data) {
    });
    $("#showhrdeptbutton").on("click", function () { FetchingData("hrdept");  });
    $("#stnchrdeptbutton").on("click", function () { FetchingSyncData("hrdept"); });
    $("#showhremployeebutton").on("click", function () { FetchingData("hremployee");});
    $("#stnchremployeebutton").on("click", function () { FetchingSyncData("hremployee"); });

    function FetchingData(type) {
        var me = this;
        switch (type) {
            case "hrdept":
                $("#hrdeptiframe").css("display", "inline-block");
                break;
            case "hremployee":
                $("#hremployeeiframe").css("display", "inline-block");
                break;
        } 
    };
    
    function FetchingSyncData(type) {
        var me = this;
        var url = "";
        if(type == "hrdept"){
        	url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchDeptByLIIPGoToLabStar6';
		    syncdata(url,"Dept");
        	return;
        }
        var checkbox = $("#checkboxs>input[type=checkbox]:checked");
        if(checkbox.length == 0){
        	layer.msg('请选择要同步的表！');
        	return;
        }else{
        	for(var i=0;i<checkbox.length;i++){
	        	switch (checkbox[i].name) {
		            case "Doctor":
		                 url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchDoctorByLIIPGoToLabStar6';
		                 syncdata(url,"Doctor");
		                 break;
		            case "Puser":
		                url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchPUserByLIIPGoToLabStar6';
		                syncdata(url,"Puser");
		                break;
	                case "NPuser":
	                	url = uxutil.path.ROOT + '/ServerWCF/Customization/RBACCloneService.svc/CatchNPUserByLIIPGoToLabStar6';
	                	syncdata(url,"NPuser");
	                 	break;
	        	}	
        	}
        	
        }
    };
    
    function syncdata(url,type){
    	uxutil.server.ajax({
            url: url
        }, function (data) {                
                if (data) {
                    if (data.success) {
                        layer.msg(type+'同步成功！');
                    } else {
                        layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
                    }
                } else {
                    layer.msg(type+'同步异常！');
                }
        });
    	
    };
});
	