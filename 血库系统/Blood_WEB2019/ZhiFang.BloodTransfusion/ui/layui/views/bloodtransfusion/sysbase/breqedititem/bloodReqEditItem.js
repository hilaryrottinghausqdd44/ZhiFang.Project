layui.extend(
	{
		uxutil:'ux/util',
		dataadapter:'ux/dataadapter',
		gridpanel: 'ux/gridpanel'
	}
).use(['uxutil', 'form','table', 'dataadapter', 'gridpanel'], function(){
	"use strict";
  	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form,
		layer = layui.layer,
        table = layui.table,
        gridpanel = layui.gridpanel,
        dataadapter = layui.dataadapter,
        addurl= uxutil.path.ROOT +"/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqEditItem",
        editurl= uxutil.path.ROOT +"/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqEditItemByField";
    
    var fieldConfig = {
    	elem: '#bloodReqEditItem',
    	where:'',
    	height:'full-30',
    	page: true,
		limit:10,
		toolbar: "#LAY-app-table-toolbar-bloodReqEditItem",
		url: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqEditItemByHQL?isPlanish=true",
		cols: [
				[{
					type: 'numbers',
					sort: true,
					width: 55,
					title: '序号'
				},{
					field: 'BloodBReqEditItem_Id',
					sort: true,
					width: 150,
					title: '项目 编码'					
				},{
					field: 'BloodBReqEditItem_BtestItemName',
					sort: true,
					width: 160,
					title: '项目名称'
				},{
					field: 'BloodBReqEditItem_LisCode',
					sort: true,
					width: 130,
					title: 'LIS编码'
				},{
					field: 'BloodBReqEditItem_DispOrder',
					sort: true,
					width: 100,
					title: '显示序号'
				},{
					field: 'BloodBReqEditItem_Visible',
					sort: true,
					width: 100,
					title: '是否使用'
				},{
					field: 'BloodBReqEditItem_DataAddTime',
					sort: true,
					width: 180,
					title: '加入时间'
				}]],
		    response: dataadapter.toResponse(),		
			parseData: function(res) {
				var result = dataadapter.toList(res);
				return result;

			}
    };
    
    //获取新增的实体参数
    function getAddParams(field) {
		var entity = {
			"BtestItemName": field.BloodBReqEditItem_BtestItemName,
			"LisCode": field.BloodBReqEditItem_LisCode,
			"DispOrder": field.BloodBReqEditItem_DispOrder,
			"Visible": field.BloodBReqEditItem_Visible
		};
		return {
			entity: entity
		};
	};
	
	//获取编辑的实体参数
    function getEditParams(field) {
		var entity = getAddParams(field);
		entity.fields = 'Id,BtestItemName,LisCode,DispOrder,Visible';
        entity.entity.Id = field["BloodBReqEditItem_Id"]; 
		return entity;
	};
	
    //检索
    function onSearch(){
    	var	searchInfo = {
			isLike: true,
			fields: ['bloodbreqedititem.BtestItemName']
		},
		itemSearchValue = $('#LAY-app-table-item-txt-search').val(),
		internalWhere = gridpanel.getSearchWhere(searchInfo, itemSearchValue);
		fieldConfig.where = {"where": internalWhere};
		table.render(fieldConfig);
		fieldConfig.where = '';
    }
    
    //保存
    function onSave(data)
    {
    	var url,
    	    optype,
    	    urldata = data.url;
    	var urlArr = urldata.split(',');    
    	if (urlArr.length < 1) return;
    	url = urlArr[0];
    	optype = urlArr[1];
    	var params = optype == 'add' ? getAddParams(data) : getEditParams(data);
    	params = JSON.stringify(params);
		//显示遮罩层
		var config = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
			    layer.msg("保存成功！");
			} else {
				layer.msg(data.msg);
			}
		});
    }
    
    //渲染完成初始化表单元素
    fieldConfig.done = function(res, curr, count)
    {
    	if (count === 0) return;
    	form.val("bloodReqEditItem-form", res.data[0]);
    }

    table.on('toolbar(bloodReqEditItem-table)', function(obj){
        var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
		switch(layEvent){
			case "add":
			    $("#form-save").attr("event-url", addurl + ',' + layEvent);
			    break;
			case "edit":
			    $("#form-save").attr("event-url", editurl + ',' + layEvent);
			    break; 
			case "delete":
			    break;
			case "search":
			    onSearch();
			    break;
		}

    })
    
    //监听提交事件
    form.on('submit(save)', function(data){
      var OpUrl = $("#form-save").attr("event-url"); //服务URL	
      //alert(OpUrl);	
      data.field.url = OpUrl;
      onSave(data.field);
//    layer.alert(JSON.stringify(data.field), {
//      title: '最终的提交信息'
//    })
      return false;
    })
    
    //监听表格单击事件,赋值表单元素
    table.on("row(bloodReqEditItem-table)", function(obj){
    	form.val("bloodReqEditItem-form", obj.data);
    })
    
    table.render(fieldConfig);
})
