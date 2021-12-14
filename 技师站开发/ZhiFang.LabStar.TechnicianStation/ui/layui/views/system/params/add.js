layui.extend({
	uxutil:'/ux/util',
	uxtable:'ux/table',
	commonzf:'modules/common/zf'
}).use(['form','uxutil','uxtable','commonzf','table'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		commonzf =layui.commonzf,
		table = layui.table,
		uxtable = layui.uxtable;
		
	var params = uxutil.params.get(true);
	//站点类型查询
	var SELECT_HOSTYPE_URL= uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeByHQL?isPlanish=true&fields=BHostType_Id,BHostType_CName';
    //检验小组查询
	var SELECT_LBSECTION_URL= uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true&fields=LBSection_Id,LBSection_CName';
    //仪器查询
	var SELECT_LBEQUIP_URL= uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true&fields=LBEquip_Id,LBEquip_CName';
    //就诊类型查询
	var SELECT_LBSICKTYPE_URL= uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true&fields=LBSickType_Id,LBSickType_CName';
    //样本类型查询
	var SELECT_LBSAMPLETYPE_URL= uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true&fields=LBSampleType_Id,LBSampleType_CName';
    //专业查询
	var SELECT_LBSPECIALTY_URL= uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSpecialtyByHQL?isPlanish=true&fields=LBSpecialty_Id,LBSpecialty_CName';
	//与操作者相关，先就考虑为检验人员，员工（身份=检验者的）
	var GET_EMP_LIST_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true&fields=HREmpIdentity_HREmployee_Id,HREmpIdentity_HREmployee_CName&where=hrempidentity.TSysCode='1001001'";
    //保存系统个性参数设置
    var SAVE_URL = uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SaveSystemParaItem';
 
    //默认设置页签数据
    var DEFALUTLIST = parent.afterUpdate1();
    //个性设置列表数据,用于过滤列表行
    var BPARAITEMLIST = parent.afterUpdate2();
    //列表选中行
    var CHECKROWDARA = [];
    //站点类型ID
    var OBJECTID = params.OBJECTID;
    
    //个性类型列表实例
	var table0_Ind = uxtable.render({
		elem:'#table',
		height:'full-80',
		title:'个性列表',
		size: 'sm', //小尺寸的表格
		cols: [[
		    {type:'checkbox', fixed: 'left'},
			{field:'Id',title:'Id',width:150,sort:false,hide:true},
			{field:'CName',title:'名称',minWidth:150,flex:1,sort:false},
			{field:'Status',title:'Status',width:100,sort:false,hide:true}
		]],
		loading:false,
		done:function(res,curr,count){
			if(count==0){
				CHECKROWDARA=[];
				return;
			}
			setTimeout(function(){
				var tr = table0_Ind.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}
			},0);
			//去掉全选按钮
			$('th[data-field=0] div').replaceWith('<div class="layui-table-cell laytable-cell-5-0-0"><span></span></div>');

			for (var i in res.data) {
				var item = res.data[i];
				if (item.Status == '1') {// 这里是判断需要禁用的条件（如：状态为1的）
					$('tr[data-index=' + i + '] input[type="checkbox"]').prop('disabled', true);
					form.render();// 重新渲染一下
				}
			}
		}
	});
	//列表查询
	function onSearch0(systemTypeCode){
		table0_Ind.instance.reload({data:[]});//清空分组列表数据
		var info = getClassInfoByKeyAndValue('Id',systemTypeCode);
		if(!info)return false;
		var index = layer.load();
		//获取列表
		onLoadTypeList(systemTypeCode,info,function(data){
			layer.close(index);
			var arr = data.value.list || [];
			var list = [];
			var info = getClassInfoByKeyAndValue('Id',systemTypeCode);
			var result = JSON.stringify(arr);
			
            switch (info.Name){
    			case '检验小组相关':
    			   var obj = result.replace(/LBSection_/g,"");
    			   list = JSON.parse(obj); 
    			   break;
    			case '检验站点相关':
    			   var obj = result.replace(/BHostType_/g,"");
    			   list = JSON.parse(obj);
    			   break;
    			case '操作者相关':
    			   var obj = result.replace(/HREmpIdentity_HREmployee_/g,"");
    			   list = JSON.parse(obj);
    			   break;
    			case '检验仪器相关':
    			   var obj = result.replace(/LBEquip_/g,"");
    			   list = JSON.parse(obj);
    			   break;
    			case '就诊类型相关':
    			   var obj = result.replace(/LBSickType_/g,"");
    			   list = JSON.parse(obj);
    			   break;
    			case '样本类型相关':
    			    var obj = result.replace(/LBSampleType_/g,"");
    			   list = JSON.parse(obj);
    			   break;
    			case '检验专业相关':
    			   var obj = result.replace(/LBSpecialty_/g,"");
    			   list = JSON.parse(obj);
    			   break;
    			default:
    				break;
    		} 
			if(data.success){
				var arr2 = [];
				for(var i=0;i<list.length;i++){
					list[i].Status= "0";
					for(var j = 0; j<BPARAITEMLIST.length;j++){
						if(list[i].Id == BPARAITEMLIST[j].BParaItem_ObjectID){
							list[i].Status='1';
							break;
						}
					}
					arr2.push(list[i]);
				}
				table0_Ind.instance.reload({data:arr2});
			}else{
				table0_Ind.instance.config.instance.layMain.html('<div class="layui-none">' + data.msg + '</div>');
			}
		});
	}
	
	//根据类名+字典内容(key+value)获取字典内容
	function getClassInfoByKeyAndValue(key,value){
		var	classInfo = commonzf.classdict.Para_SystemType,
			data = null;
		for(var i in classInfo){
			if(classInfo[i][key] == value){
				data = classInfo[i];
				break;
			}
		}
		return data;
	}
		//获取类型列表
    function onLoadTypeList(systemTypeCode,info,callback){
    	var url  ='';
    	var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
        switch (info.Name){
			case '检验小组相关':
			   url =  SELECT_LBSECTION_URL;
			   if(OBJECTID)url+='&where=lbsection.Id='+OBJECTID;
               parent.layer.title('新增检验小组', index);  //再改变当前层的标题
			   document.getElementById("spanbadge").innerHTML = "点击选择一个小组";
			   break;
			case '检验站点相关':
			   url =  SELECT_HOSTYPE_URL;
			   if(OBJECTID)url+='&where=bhosttype.Id='+OBJECTID;
			   parent.layer.title('新增站点类型', index); 
			   document.getElementById("spanbadge").innerHTML = "点击选择一个站点";
			   break;
			case '操作者相关':
			    url = GET_EMP_LIST_URL;
			    if(OBJECTID)url+='&where=hrempidentity.Id='+OBJECTID;
			    parent.layer.title('新增操作者', index);  
				document.getElementById("spanbadge").innerHTML = "点击选择一个操作者";
			   break;
			case '检验仪器相关':
			    url =  SELECT_LBEQUIP_URL;
			    if(OBJECTID)url+='&where=lbequip.Id='+OBJECTID;
				parent.layer.title('新增仪器', index);  
			    document.getElementById("spanbadge").innerHTML = "点击选择一个仪器";
			   break;
			case '就诊类型相关':
			   url =  SELECT_LBSICKTYPE_URL;
			   if(OBJECTID)url+='&where=lbsicktype.Id='+OBJECTID;
			   parent.layer.title('新增就诊类型', index);  
			   document.getElementById("spanbadge").innerHTML = "点击选择一个就诊类型";
			   break;
			case '样本类型相关':
			   url =  SELECT_LBSAMPLETYPE_URL;
			   if(OBJECTID)url+='&where=lbsampletype.Id='+OBJECTID;
		       parent.layer.title('新增样本类型', index);  
			   document.getElementById("spanbadge").innerHTML = "点击选择一个样本类型";
			   break;
			case '检验专业相关':
			   url = SELECT_LBSPECIALTY_URL;
			   if(OBJECTID)url+='&where=lbspecialty.Id='+OBJECTID;
			   parent.layer.title('新增专业', index);
			   document.getElementById("spanbadge").innerHTML = "点击选择一个专业";
			   break;
			default:
				break;
		} 
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	}
    table0_Ind.table.on('row(table)', function(obj){
    	CHECKROWDARA=[];
    	CHECKROWDARA.push(obj.data);
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	});
	//保存
    $('#save').on('click',function(){     	
    	onSaveClick();
    });
    //取消
    $('#cancel').on('click',function(){     	
    	parent.layer.closeAll('iframe');
    });
    
    //保存
	function onSaveClick(){
		var checkStatus = table.checkStatus('table'),
	        data1 = checkStatus.data;
	   
		if(data1.length==0){
			layer.msg('请勾选列表数据行');
			return;
		}
		if(DEFALUTLIST.length==0){
			layer.msg('没有参数数据不能保存');
			return;
		}
		var entityList=[];
		for(var i=0;i<DEFALUTLIST.length;i++){
			var obj ={
				ParaNo : DEFALUTLIST[i].BPara_ParaNo,
				IsUse:1,
				ParaValue:DEFALUTLIST[i].BPara_ParaValue
			};
			if(DEFALUTLIST[i].BPara_Id){
				obj.BPara={
	        		Id:DEFALUTLIST[i].BPara_Id,
	        		DataTimeStamp:[0,0,0,0,0,0,0,0]
	        	};
			}
			if(uxutil.cookie.get(uxutil.cookie.map.USERID)){
	        	obj.OperatorID = uxutil.cookie.get(uxutil.cookie.map.USERID);
	        	obj.Operater =uxutil.cookie.get(uxutil.cookie.map.USERNAME);
	        }
			entityList.push(obj);
		}
		var objectInfo =[];
	    for(var i=0;i<data1.length;i++){
	    	objectInfo.push({
	    		ObjectID:data1[i].Id,
				ObjectName:data1[i].CName,
	    	});
	    }    
		var params={
			objectInfo:JSON.stringify(objectInfo),
			entityList:entityList
		};
		if (!params) return;
		params = JSON.stringify(params);
	
		var config = {
			type: "POST",
			url:SAVE_URL,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config, function(data) {
			layer.close(index);
			if (data.success) {
				layer.msg("保存成功",{icon:6,time:2000});
                parent.layer.closeAll('iframe');
				parent.afterUpdate(data);
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
    }
	//初始化
	function init(){
	    commonzf.classdict.init('Para_SystemType',function(){
	   	    if(!commonzf.classdict.Para_SystemType){
		   		layer.alert('未获取到系统相关性')
    			return;
			}
	   	    onSearch0(params.SYSTEMTYPECODE);
		});
	};
	init();
});