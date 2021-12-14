layui.extend({
	uxutil:'ux/util',
	dataadapter: 'ux/dataadapter',
	nursesconfig: 'views/bloodtransfusion/nursestation/config/nursesconfig',
}).define(['form', 'table', 'uxutil', 'dataadapter', 'laytpl', 'layer', 'nursesconfig'], function(exports){
	"use strict";
	var $ = layui.jquery;
	var form = layui.form;
	var table = layui.table;
	var layer = layui.layer;
	var laytpl = layui.laytpl;
	var uxutil = layui.uxutil;
	var nursesconfig = layui.nursesconfig;
	var dataadapter = layui.dataadapter;
	var _itemName = 'name', _aboName = 'aboname', _bagCode = 'bagcode';
	var _reqformid= 'reqformid', _bloodstyleid = 'bloodstyleid', _boutitemid = 'boutitemid';
	var _boutfromid = 'boutfromid', _appearance = 'appearance', _integrity = 'integrity';
	
	//当前操部门
    var deptId = uxutil.cookie.get(uxutil.cookie.map.DEPTID) || "";
    var deptName = uxutil.cookie.get(uxutil.cookie.map.DEPTNAME) || "";

    var urlParams = uxutil.params.get() || {};
    var admId = urlParams["admId"] ? urlParams["admId"] : ""; //就诊号或者登记号
    
	//血袋接收状态
	var bagHandoverStaus = [
	    '<div class="layui-inline {{# if(d.BloodBOutItem_HandoverCompletion != 3){ }}layui-hide{{# }}}" ',
		'id="{{d.BloodBOutItem_Id + "-input"}}" > ',
		'<input class="layui-input " type="checkbox" lay-skin="primary" ',
		'id="{{d.BloodBOutItem_Id + "-check"}}" disabled="disabled" ',
		'{{# if(d.BloodBOutItem_HandoverCompletion == 3){ }}checked="true"{{# }}}" />',
		'</div>'];

	
	//血袋模板项目信息
	var bagItems = [
		'<div class="layui-inline" id="{{d.BloodBOutItem_Id} + "-item"}"> ',
	    '<div name="name"><span>{{d.BloodBOutItem_Bloodstyle_CName}}</span></div>',
	    '<div name="aboname"><span>{{d.BloodBOutItem_BloodABO_CName}}{{"("+ d.BloodBOutItem_BloodABO_RHType +")"}}</span></div>',
	    '<div name="bagcode"><span>{{d.BloodBOutItem_BBagCode}}</span></div>',
	    '<div name="b3code"><span>{{d.BloodBOutItem_B3Code}}</span></div>',
	    '<div name="reqformid" class = "layui-hide"><span>{{d.BloodBOutItem_BloodBReqItem_BReqFormID}}</span></div>',
	    '<div name="bloodstyleid" class = "layui-hide"><span>{{d.BloodBOutItem_Bloodstyle_Id}}</span></div>',
	    '<div name="boutitemid" class = "layui-hide"><span>{{d.BloodBOutItem_Id}}</span></div>',
	    '<div name="boutfromid" class = "layui-hide"><span>{{d.BloodBOutItem_BloodBOutForm_Id}}</span></div>',
		'</div>'];
	    
	//血袋登记按钮   
	var bagRegisterbtn = [
		'<div class="layui-inline {{# if(d.BloodBOutItem_HandoverCompletion == 3){}}layui-hide{{#}}}" ' ,
	    'id="{{d.BloodBOutItem_Id + "-btn"}}"> ',
	    '<button class="layui-btn layui-btn-danger layui-btn-lg" ', 
	    'outitemid="{{d.BloodBOutItem_Id}}">登记</button> ' ,
	    '</div>'];
	
	//血袋登记结果信息
	var bagOperResult =[
		'<div class="layui-inline {{# if(d.BloodBOutItem_HandoverCompletion != 3){}}layui-hide{{#}}}" ' ,
	    'id="{{d.BloodBOutItem_Id + "-result"}}"> ',
	    '<div name="appearance"><span>{{"外观：" + d.BloodBOutItem_BloodAppearance_BDict_CName}}</span></div>',
	    '<div name="integrity"><span>{{"完整：" + d.BloodBOutItem_BloodIntegrity_BDict_CName}}</span></div>' ,
	    '</div>'];
	   
	//血袋模板
	var bagItemTemplate = '<div '
	    + 'class="layui-form-item blooditem {{# if(d.BloodBOutItem_HandoverCompletion == 3){}}blooditem-select{{#}}}" '
	    + 'id="{{d.BloodBOutItem_Id}}"> '
      	+	bagHandoverStaus.join('')
        +	bagItems.join('')
     	+	bagRegisterbtn.join('')
     	+   bagOperResult.join('') 
        +'</div>';
        
	//代理对象
	var boutitemtableProxy = {
		    bagItemEleId: "#boutitem_list",
		    bagCodeEleId: "#search_barcode",
		    //缓存扫码数据
		    cacheScandata:{},
   	        config: {
   	        	fields:['BloodBOutItem_Id', 
    			   'BloodBOutItem_BBagCode', 
    			   'BloodBOutItem_B3Code', 
    			   'BloodBOutItem_Bloodstyle_CName', 
    			   'BloodBOutItem_BloodABO_CName',
    			   'BloodBOutItem_BloodABO_RHType',
    			   'BloodBOutItem_HandoverCompletion',  //完成度3已完成
    			   'BloodBOutItem_BloodAppearance_BDict_CName',  //血袋外观信息
    			   'BloodBOutItem_BloodIntegrity_BDict_CName',   //血袋完整性
    			   'BloodBOutItem_BloodBOutForm_Id', //提交时用到
    			   'BloodBOutItem_BloodBReqItem_BReqFormID',  //提交时用到
    			   'BloodBOutItem_Bloodstyle_Id'  //提交时用到 
    			],
    			bagfields:['BloodBOutItem_Id', 
    			   'BloodBOutItem_BBagCode', 
    			   'BloodBOutItem_B3Code', 
    			   'BloodBOutItem_Bloodstyle_CName', 
    			   'BloodBOutItem_BloodABO_CName',
    			   'BloodBOutItem_BloodABO_RHType',
    			   'BloodBOutItem_BloodAppearance_BDict_CName',  //血袋外观信息
    			   'BloodBOutItem_BloodIntegrity_BDict_CName',   //血袋完整性    			   
    			   'BloodBOutItem_HandoverCompletion',  //完成度3已完成
    			   'BloodBOutItem_BloodBReqItem_BReqFormID',  //提交时用到
    			   'BloodBOutItem_Bloodstyle_Id', //提交时用到
    			   //发血主单，但是血袋总数没见
    			   'BloodBOutItem_BloodBOutForm_Id', //提交时用到
       			   'BloodBOutItem_BloodBOutForm_CheckTime',
    			   'BloodBOutItem_BloodBOutForm_HandoverCompletion',
    			   //病人信息
    			   'BloodBOutItem_BloodBReqForm_AdmID',
    			   'BloodBOutItem_BloodBReqForm_PatNo',
    			   'BloodBOutItem_BloodBReqForm_CName',
    			   'BloodBOutItem_BloodBReqForm_Sex',
    			   'BloodBOutItem_BloodBReqForm_AgeALL',
    			   'BloodBOutItem_BloodBReqForm_DeptNo',
    			   'BloodBOutItem_BloodBReqForm_Bed'
    			],
    			params:{page: 0, limit: 10000, fields: "", where:""},
    			selectUrlByOutformId: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL?isPlanish=true',
			    selectUrlByBagCode: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfHandoverByBBagCodeHQL?isPlanish=true'
		    }			
	};
	
	//构造器,通过代理对象生成table对象
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, boutitemtableProxy.config, me.config, options);
		var inst = $.extend(true, {}, boutitemtableProxy, me); // table,
		inst.config.url = inst.config.selectUrl;
		return inst;
	}; 
	
	//获取血袋明细信息id
	boutitemtableProxy.getOutItemIdByElement = function(ele){
		var me = this;
		//如果是目标节点不循环
		if (ele.className && ele.className.indexOf('blooditem') !== -1 && ele.id){
	    	return ele.id
	    };
	    ele = ele.parentNode || ele.parentElement;
	    while (ele){
	    	if (ele.className && ele.className.indexOf('blooditem') !== -1 && ele.id){
	    		return ele.id
	        };
	        ele = ele.parentNode || ele.parentElement;
	    };
	};
	
	//解析扫码返回的数据，根据发血主单id缓存数据
	boutitemtableProxy.parserScanBagdata = function(res){
		var me = this;
		var row = {};
		var outformid = '';
		me.cacheScandata = {}; //清空数数据
		for (var i = 0; i < res.length; i++){
			outformid = res[i]["BloodBOutItem_BloodBOutForm_Id"];
			me.cacheScandata[outformid] = me.cacheScandata[outformid] ? me.cacheScandata[outformid] : [];
			row = new Object();
			for (var j = 0; j < me.config.bagfields.length; j++){
				row[me.config.bagfields[j]] = res[i][me.config.bagfields[j]];
			};
			me.cacheScandata[outformid].push(row);
		};
		return res[0]["BloodBOutItem_BloodBOutForm_Id"]; //返回第一个发血主单
	};
	
	//根据发血主单ID获取发血明细
	boutitemtableProxy.loadBoutItemByOutformId = function(outformId, callback){
     	var me = this;
    	var where = [];
    	where.push("bloodboutitem.BloodBOutForm.Id='" + outformId + "'");
    	var fields =  me.config.fields.join(',');
    	var data = me.config.params || {};
	    data["fields"] = fields;
	    data["where"] = '( ' + where.join(' and ') + ' ) ';
    	var config= {
    		type: 'get',
        	url: me.config.selectUrlByOutformId,
        	data: data
    	};
    	//查询数据
		uxutil.server.ajax(config, function(data) {
			if (data.success){
				me.SetBloodbagItems(data.value.list);
				if (callback) callback();
			}else{
			  layer.msg("查询血袋数据错误！" + data.msg, {time: 3000});
			};
		});   		
	};
	
	//根据血袋号获取用血项目
    boutitemtableProxy.loadBoutItemByBagcode = function(bagCode, callback){
     	var me = this;
     	//根据就诊号判断使用查询条件
    	var where = admId ? 'bloodboutitem.BloodBOutForm.BloodBReqForm.AdmID = ' + "'" + admId + "'" 
    	    : 'bloodboutitem.BloodBOutForm.BloodBReqForm.DeptNo = ' + deptId ;
    	var fields =  me.config.bagfields.join(',');
		var data = me.config.params || {};
		data["fields"] = fields;
		data["where"] = where;
		data['bagCode'] = bagCode;
    	var config= {
    		type: 'get',
        	url: me.config.selectUrlByBagCode,
        	data: data
    	};
    	//查询数据
		uxutil.server.ajax(config, function(data) {
		    if (data.success){
		   		if (callback && typeof callback == 'function') callback(data.value.list);
		    } else {
		   		layer.msg("查询血袋数据错误！" + data.msg, {	time: 3000});
		    };
		});   	
    };
    
	//获取条码数据
	boutitemtableProxy.getDocBagCodeByScan = function(){
		var me = this;
		var LBarcode = $(me.bagCodeEleId);
		if (LBarcode.length <= 0) return;
		return LBarcode.val();
	};
	
	//根据发血明细Id和项目名称取得项目值
	boutitemtableProxy.getDocBagItemValueByName = function(outitemId, name){
		var me = this;
		var bOutitemId = '#' + outitemId;
		var Item = $(bOutitemId);
		if (Item.length <= 0) return;	
	    var Tempitem = Item.find("div[name=" + name + "]"); 
		return Tempitem.find("span") ? Tempitem.find("span").text() : '';
	};
	
	//同过发血明细ID获取项目信息
	boutitemtableProxy.getDocItemDataByOutItemId = function(outitemId, data){
		var me = this;
		data = data || {};
		data["CName"] = me.getDocBagItemValueByName(outitemId, _itemName);
		data["ABOName"] = me.getDocBagItemValueByName(outitemId, _aboName);
		data["BBagCode"] = me.getDocBagItemValueByName(outitemId, _bagCode);
		data["BReqFormID"] = me.getDocBagItemValueByName(outitemId, _reqformid);
		data["BloodNo"] = me.getDocBagItemValueByName(outitemId, _bloodstyleid);
		data["BOutItemID"] = me.getDocBagItemValueByName(outitemId, _boutitemid);
		data["BOutFormID"] = me.getDocBagItemValueByName(outitemId, _boutfromid);		
		return data;
	};
	
	//生成模板数据
	boutitemtableProxy.CreateTemplateData = function(templatehtml, data){
		var Lhtml = '';
		laytpl(templatehtml).render(data, function(html){
			Lhtml = html;
		});
		return Lhtml;		
	};
	
	//设置血袋明细数据,参数为数组对象
	boutitemtableProxy.SetBloodbagItems = function(data, callback){
		var me = this;
		var Ldata = {};
		var Lhtm = '';
		data = data || [];
		for(var i = 0; i < data.length; i++) {
			Ldata = data[i] || {};
			Lhtm = Lhtm + me.CreateTemplateData(bagItemTemplate, Ldata)
		};
		var Lview = $(me.bagItemEleId).empty().append(Lhtm);
		form.render('checkbox');
		if (callback && typeof callback == 'function') callback();
	};
	
	//清除血袋明细信息数据
	boutitemtableProxy.clearBagItem = function(){
		var me = this;
	  	var Lview = $(me.bagItemEleId);
	  	if (Lview) Lview.empty();
	};
	
	//去除所有血袋颜色
	boutitemtableProxy.ClearItemColor =function(){
		var me = this;
		var Lview = $(me.bagItemEleId);	
		if (Lview.length <= 0) return; 
		var Litems = Lview.find("div[class*='blooditem']");
		if (Litems.length <= 0) return; 
		Litems.removeClass('blooditem-select');
	};
	
	//显示隐藏checkbox
	boutitemtableProxy.setCheckboxVisble = function(outitemId, Visble){
		var me = this;
        var Lview = $(me.bagItemEleId);
        if (Lview.length <= 0) return; 
 		var LinputId = '#' + outitemId + "-input";
  		var Linput = Lview.find(LinputId);
  		if (Linput.length <= 0) return; 
 		if (Visble) {
 			Linput.removeClass('layui-hide');
 		} else {
 		  Linput.addClass('layui-hide');
 		};
	};

	//设置checkbox选择状态
	boutitemtableProxy.setCheckboxChecked = function(outitemId, checked){
		var me = this;
        var Lview = $(me.bagItemEleId);
        if (Lview.length <= 0) return; 
 		var LinputId = '#' + outitemId + "-input";
  		var Linput = Lview.find(LinputId);
  		if (Linput.length <= 0) return; 
  		var LcheckboxId = '#' + outitemId + "-check";
  		var LCheckbox = Linput.find(LcheckboxId);
  		if (LCheckbox.length <= 0) return;
  		LCheckbox.attr('checked', checked);
	};
	
	//显示隐藏Register button 登记按钮
	boutitemtableProxy.setRegisterButtonVisble = function(outitemId, Visble){
		var me = this;
        var Lview = $(me.bagItemEleId);
        if (Lview.length <= 0) return; 
 		var buttonId = '#' + outitemId + "-btn";
  		var Lbutton = Lview.find(buttonId);
  		if (Lbutton.length <= 0) return; 
 		if (Visble) {
 			Lbutton.removeClass('layui-hide');
 		} else {
 		    Lbutton.addClass('layui-hide');
 		};
	};
	
	//设置血袋信息外观和完整性
	boutitemtableProxy.setBagOperResult = function(outitemId, data){
		var me = this;
        var Lview = $(me.bagItemEleId);
        if (Lview.length <= 0) return; 
 		var BagOperId = '#' + outitemId + "-result";
  		var BagResult = Lview.find(BagOperId);
  		if (BagResult.length <= 0) return; 
 	 	BagResult.removeClass('layui-hide');
        var Litem = BagResult.find("div[name=" + _appearance + "]");
        if (Litem.length <= 0) return; 
		Litem.html('外观：' + data[_appearance]);
        Litem = BagResult.find("div[name=" + _integrity + "]");
        if (Litem.length <= 0) return; 
		Litem.html('完整：' +data[_integrity]);		
	};
	
	//删除血袋节点
	boutitemtableProxy.deleteBagItem = function(outitemId){
		var me = this;
        if (Lview.length <= 0) return; 
        var boutitemId = '#'+ outitemId ;
 		var Litem = Lview.find(boutitemId);
		if (Litem.length > 0) {	
			Litem.remove(boutitemId);
		};
	};
	
	//根据发血明细ID设置某个血袋的颜色，数据交接提交成功后设置
	boutitemtableProxy.SetItemColor = function(outitemId, data){
		var me = this;
		var LColor = 'blooditem-select';
		var LoutitemId = '#'+ outitemId;
        var Lview = $(me.bagItemEleId);
        if (Lview.length <= 0) return; 
 		var Litem = Lview.find(LoutitemId);
		if (Litem.length > 0) {
			Litem.addClass(LColor);
            me.setCheckboxVisble(outitemId, true);
            me.setCheckboxChecked(outitemId, true)
            me.setRegisterButtonVisble(outitemId, false);
            me.setBagOperResult(outitemId, data);
			form.render('checkbox');				
		} else {
			layer.msg('没有找到该条码的血袋信息！' + LbagCode, {
				time: 3000
			});
		};       
	};
	
	//扫码事件
	boutitemtableProxy.onScancodeKeydown = function(callback){
		//检索数据，根据血袋号查询数据
		var me = this;
		var bagCode = me.getDocBagCodeByScan();
		if (!bagCode || bagCode == "") return;
		me.loadBoutItemByBagcode(bagCode, function(data){
		   	if (callback && typeof callback == 'function') callback(bagCode, data);
		});
	};
	
	//设置双击血袋项目事件，通过event.target取到血袋号
	boutitemtableProxy.setbagItemDbClickEvent = function(onDbClickEvent){
		var me = this;
		var Lview = $(me.bagItemEleId);	
		if (Lview.length <= 0) return; 
		var Litems = Lview.find("div[class*='blooditem']"); //
		if (Litems.length <= 0) return; 
		Litems.on('dblclick', onDbClickEvent); //dblclick
	};
	
	//设置血袋信息登记按钮事件
	boutitemtableProxy.setbagRegisterClickEvent = function(onClickEvent){
		var me = this;
		var Lview = $(me.bagItemEleId);	
		if (Lview.length <= 0) return; 
		var Litems = Lview.find('.layui-btn.layui-btn-danger.layui-btn-lg');
		if (Litems.length <= 0) return; 
		Litems.on('click', onClickEvent);
	};	
	//暴露接口
	var boutitemtable = new Class();
	//这里的boutformtable跟extend定义必须一致
	exports("boutitemtable", boutitemtable);
});