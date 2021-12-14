layui.extend({
	uxutil: 'ux/util'
}).use(['form','laydate','uxutil'], function(){
    var form = layui.form,
        uxutil =layui.uxutil,
        laydate = layui.laydate;
    
    //日期选择器
    laydate.render({
       elem: '#LAY-user-birthday'
    });
    form.render('radio');
    //查询员工服务
	var SELECT_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeById?isPlanish=true';
    //修改员工服务
    var EDIT_URL = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField';
    //查询性别
    var SELECT_SEX_URL = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBSexByHQL?isPlanish=true';
      //性别加载
    loadSexData();
    //加载
    loadInfoData();

    //确认修改
	form.on('submit(LAY-user-setinfo)',function(obj){
		onEdit(obj);
	});
    
    //重新填写
	form.on('submit(LAY-user-setreset)',function(obj){
		loadInfoData();
	});

    function loadInfoData(){
		var url = SELECT_URL + '&id=' + uxutil.cookie.get(uxutil.cookie.map.USERID)+
		'&fields='+getStoreFields().join(',');
		//请求登入接口
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				form.val('employee',changeResult(data.ResultDataValue));
			}else{
				layer.msg(data.msg);
			}
		});
	}
    function loadSexData(){
		var url = SELECT_SEX_URL + '&bsex.IsUse=1'+
		'&fields=BSex_Name,BSex_Id';
		//请求登入接口
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
				var tempAjax = '';
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].BSex_Id + "'>" + value.list[i].BSex_Name + "</option>";
                    $("#LAY-user-sex").empty();
                    $("#LAY-user-sex").append(tempAjax);
              
                }
                form.render('select');//需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
	}
    function onEdit(obj){
    	var indexs=layer.load(2);
    	var params={
			Id: obj.field.HREmployee_Id,
			Birthday: obj.field.HREmployee_Birthday ? JcallShell.Date.toServerDate(obj.field.HREmployee_Birthday) : null,
            Comment: obj.field.HREmployee_Comment,
            MobileTel: obj.field.HREmployee_MobileTel,
            NameF: obj.field.HREmployee_NameF ? obj.field.HREmployee_NameF : ' ',
			NameL: obj.field.HREmployee_NameL ? obj.field.HREmployee_NameL : ' ',
			Address:obj.field.HREmployee_Address,
            IsEnabled: 1,
			IsUse: true
		};
		params.BSex={
			Id:obj.field.HREmployee_Sex,
			DataTimeStamp:[0,0,0,0,0,0,0,0]
		};
    	var entity = {
			entity: params,
			fields:'Id,Comment,MobileTel,Birthday,NameF,NameL,IsEnabled,IsUse,Address,BSex_Id'
		};
    	JcallShell.Server.ajax({
			type:'post',
			url: EDIT_URL,
			data:JcallShell.JSON.encode(entity)
		}, function(data){
			layer.close(indexs);
			if(data.success == true){
				layer.msg('修改成功',{icon:1,time:2000});
			}else{
                layer.msg(data.msg, {icon: 2});
			}
		});
    }
    /**@overwrite 返回数据处理方法*/
	function changeResult(data){
		var list =  JSON.parse(data);
//		list.HREmployee_Birthday = JcallShell.Date.toString(list.HREmployee_Birthday,true);
		list.HREmployee_Sex=list.HREmployee_BSex_Id;
		
		 console.log(list);
		 
		return list;
	}
	/**创建数据字段*/
	function getStoreFields(){
		var fields = [];
			
		$(":input").each(function(){ 
			if(this.name)fields.push(this.name)
	    });
		return fields;
	}
	
});

