layui.extend({
	uxutil:'ux/util'
}).use(['uxutil'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil;
		
	//账号申请账号表查询服务
	var GET_SACCOUNT_REGISTER_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchSAccountRegisterByHQL?isPlanish=true';

    //查询
	$('#search_button').click(function(){
		var value = $('#Account').val();
		if(!value){
			layer.msg('请先输入账号',{ icon: 5, anim: 6 ,time:2000});
			return false;
		}
		 //根据账号查申请状态
		getStatusByAccount(value,function(html){
			document.getElementById("status_msg").innerHTML = html;
		});
	});
	//取消
	$('#cancel').click(function(){
		parent.layer.closeAll('iframe');
	});
	
    //根据账号查申请状态
    function getStatusByAccount(account,callback){
    	var where ="saccountregister.Account='"+ account+"'";
		var url = GET_SACCOUNT_REGISTER_LIST_URL+'&fields=SAccountRegister_StatusId&where='+where;
        var layerIndex = layer.load();//开启保护层
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.close(layerIndex);//关闭保护层
			if(data.success){
				var list = data.value.list || [];
				var msg = '<div class="layui-none" style="color: red;font-weight:bold;font-size:large;">该账号【'+account+'】不存在,请注册申请。</div>';
				if(list.length>0){
					msg = '<div style="font-weight: bold ;color: #FFB800;font-size: large;">该账号【'+account+'】正在申请中,请等待...</div>';
					if(list[0].SAccountRegister_StatusId=='2')msg='<div class="layui-none" style="color:red;font-weight:bold;font-size:large;">该账号【'+account+'】已被审批打回,请重新申请。</div>';
					if(list[0].SAccountRegister_StatusId=='1')msg='<div style="color:green;font-weight:bold;font-size:large;">该账号【'+account+'】已审批通过。</div>';
				}
				callback(msg);
			}else{
				layer.msg(data.ErrorInfo);
			}
		});
	}
});