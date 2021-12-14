layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: 'config/bloodsconfig',
	nursesconfig: 'views/bloodtransfusion/nursestation/config/nursesconfig',	
	patientInfotTable: 'views/bloodtransfusion/nursestation/bloodprohandover/patientinfotable',
	boutformtable: 'views/bloodtransfusion/nursestation/bloodprohandover/boutformtable',
	boutitemtable: 'views/bloodtransfusion/nursestation/bloodprohandover/boutitemtable',
	bagoperationfrom: 'views/bloodtransfusion/nursestation/bloodprohandover/bagoperationfrom'
}).use(['laytpl', 'form', 'layer', 'table',
    'patientInfotTable', 'boutformtable', 'boutitemtable', 
    'bloodsconfig', 'bagoperationfrom','cachedata', 'nursesconfig'], function(){
	"use strict";
	var $ = layui.jquery; //
	var form = layui.form;
	var table = layui.table;
	var laytpl = layui.laytpl;
	var bloodsconfig = layui.bloodsconfig;
	var patientInfotTable = layui.patientInfotTable;
	var boutformtable = layui.boutformtable;
	var boutitemtable = layui.boutitemtable;
	var bagoperationfrom = layui.bagoperationfrom;
	var isScanCode = false;  //使用来判断是否扫码操作还是默认装载操作
	var cachedata = layui.cachedata;
	var nursesconfig = layui.nursesconfig;
	//先按就诊号，没有按照 科室和病区， 把科室修改为患者id(就诊号)
	var SCanFieldName = nursesconfig.getScanCodeIDField(); //获取血袋扫码识别字段
	
    //禁用启用按钮
    function disabledLayerbutton(index, layero){
    	var buttonClass = '.layui-layer-btn' + index;
		var button = layero.find(buttonClass);
		var	className = "layui-btn-disabled";
		//禁用状态直接不处理
		if(button.hasClass(className)){
			return false;
		}
		button.addClass(className);
		setTimeout(function(){ 
			button.removeClass(className);
		}, 3000);   	
    };
    
    //设置确认框数据
    function setConfigData(layero, BagOperResultID){
        var data = {};
        var opdtl = []; //操作明细记录
        var framedoc = layero.find('iframe').contents();
        var ele = framedoc.find('#bloodappearance-select');  
        var appearanceId = ele.val();
        var appearanceText = ele.find('option:selected').text();
        ele = framedoc.find('#bloodintegrity-select');
        var integrityId = ele.val();
        var integrityText = ele.find('option:selected').text();
        data["BagOperResultID"] = BagOperResultID;
        opdtl.push({"Id": appearanceId, "text": appearanceText});
        opdtl.push({"Id": integrityId, "text": integrityText});
        data["bagOpDtl"] = opdtl; 
        //设置是运送人
        ele = $('#carrier_select');
        var CarrierID = ele.val();
        var Carrier = ele.find('option:selected').text();
        var CarrierName = Carrier.split('-');
        if (CarrierName.length > 1) {
        	Carrier = CarrierName[0];
        };
        data["CarrierID"] = CarrierID;
        data["Carrier"] = Carrier;
        data["appearance"] = appearanceText; //扫码保存后显示使用
        data["integrity"] = integrityText; //扫码保存后显示使用
        return data;
    };
    
	//打开确认对话框
	function openConfigDialog(callback){
		var content = 'configdialog.html';
		layer.open({			
			type: 2,
			title: "血袋交接确认",
			area: ['350px', '300px'],
			content: content,
			id: "lay-app-form-open-bloodbagconfig",
			btn: ['确定接收', '退回'],
			btnAlign:"c",
			success: function (layero, index) {
				return false; 
			},
			yes: function (index, layero) {
                disabledLayerbutton(0, layero);
                //确认接收
                var data = setConfigData(layero, 1);
                layer.close(index);
                if (callback && typeof callback == 'function') callback(data);
				return false; 
			},
			btn2: function (index, layero) {
				disabledLayerbutton(1, layero);
                //确认退回
                var data = setConfigData(layero, 2); 
                layer.close(index);
				 if (callback && typeof callback == 'function') callback(data);
				return false; 
			},
			end: function () {				

			},
			cancel: function (index, layero) {
				
			}
		});
	};
	
	//通过患者信息检索发血主单
	function onRefreshBoutformtable(data){
		var outformid = data["BloodBOutForm_Id"];
		var list = boutitemtable.cacheScandata[outformid];
        if (!isScanCode){
	        //申请信息VO
			var bReqVo = {
				AdmID : data["BloodBReqFormVO_AdmID"],
				PatNo : data["BloodBReqFormVO_PatNo"],
				CName : data["BloodBReqFormVO_CName"],
				Sex   : data["BloodBReqFormVO_Sex"],
				AgeALL: data["BloodBReqFormVO_AgeALL"],
				DeptNo: data["BloodBReqFormVO_DeptNo"],
				Bed   : data["BloodBReqFormVO_Bed"]  
			}; 
			bReqVo = JSON.stringify(bReqVo)
			boutformtable.setVoWhere(bReqVo); //设置条件
			boutformtable.render();       	
        }else if(isScanCode && list.length > 0){
            //显示扫码缓存发血主单信息
            boutformtable.showOutForminfoByScanbag(list);	
            //显示扫码缓存血袋信息
			boutitemtable.SetBloodbagItems(list, function(){
				boutitemtable.setbagRegisterClickEvent(onRegisterClick); //设置血袋信息单击事件
			    boutitemtable.setbagItemDbClickEvent(onbagItemDbClick); //设置血袋信息双击事件				
			});
        };
	};
	
	//检索发血明细
	function onRefreshBoutitemtable(data){
		var outformid = data["BloodBOutForm_Id"];
		var list = boutitemtable.cacheScandata[outformid]; //获取扫码缓存血袋信息
		//扫码不查询数据库，使用扫码返回的数据填充
		if (!isScanCode){
			//使用通用服务查询发血明细，条件是发血主单
			boutitemtable.loadBoutItemByOutformId(outformid, function(){
				boutitemtable.setbagItemDbClickEvent(onbagItemDbClick);  //设置血袋信息双击事件
		        boutitemtable.setbagRegisterClickEvent(onRegisterClick); //设置血袋信息单击事件
			});			
		} else if(isScanCode && list.length > 0){
			//显示扫码缓存血袋信息
			boutitemtable.SetBloodbagItems(list);
			boutitemtable.setbagRegisterClickEvent(onRegisterClick); //设置血袋信息单击事件
		    boutitemtable.setbagItemDbClickEvent(onbagItemDbClick); //设置血袋信息双击事件
		};
	};
	
	//按钮事件联动
	var doBunttonEvent = {
		refresh: function(){
			isScanCode = false;
			boutformtable.cleardata(); //清除发血主单数据
			boutitemtable.clearBagItem(); //清除血袋明细数据
			patientInfotTable.render(); //获取病人信息
		}
   };
	
	/**表单事件监听*/
	function onFormEvent(){
		/**表单按钮事件监听*/
		$('.layui-form .layui-form-item .layui-inline .layui-btn').on('click', function() {
			var type = $(this).data('type');
			doBunttonEvent[type] ? doBunttonEvent[type].call(this) : '';
		});	
	    //扫	码按键监听,先根据血袋检索数据，然后打开确认对话框确认
	    $('#search_barcode').on('keydown', function(event) {
			if(event.keyCode == 13) {
				isScanCode = true;
				boutitemtable.onScancodeKeydown(function(bagCode, res){
				    //缓存查询回来的数据，因为可能存在多条发血单数据，一个血袋多个病人使用
				    if (res.success){
					    var data = res.value.list;
						var outformid = boutitemtable.parserScanBagdata(data); //解释数据
						var list = boutitemtable.cacheScandata[outformid]; //获取第一个发血主单数据
						patientInfotTable.showPatinfoByScanbag(data); //显示病人信息，需要全部显示
						boutformtable.showOutForminfoByScanbag(list); //显示发血主单信息
						boutitemtable.SetBloodbagItems(list); //设置发血学贷信息
						boutitemtable.setbagRegisterClickEvent(onRegisterClick); //设置血袋信息单击事件
					   	boutitemtable.setbagItemDbClickEvent(onbagItemDbClick); //设置血袋信息双击事件
				   	}else{
				   		layer.msg("查询血袋数据错误！" + res.msg, {	time: 3000});
				   	}
                   //设置扫码为空值	
                   event.target.value = '';
	   			});
			};		
		});
	};

	//监听table事件
	function onTableEvent(){
		//患者信息列表监听监听行单击事件
		table.on('row(patient_filter)', function(obj) {
			setTimeout(function() {
				onRefreshBoutformtable(obj.data);
			}, 250);
		});	
		//发血单列表监听监听行单击事件
		table.on('row(boutform_filter)', function(obj) {
			setTimeout(function() {
				onRefreshBoutitemtable(obj.data);
			}, 250);
		});		
	};
    
    //保存血袋交接操作记录
    function SavebagOper(outitemId){
    	var outformId = '';
    	var appearanceText = '';
    	var integrityText = '';
 		var data = boutitemtable.getDocItemDataByOutItemId(outitemId);
		outformId = data["BOutFormID"]; //是扫码用来修改数据交接完成度
		openConfigDialog(function(result){
			var CarrierId = result["CarrierID"];
			if (!CarrierId) {
				layer.msg("没有选择护工，请检查！",{time: 5000});
				return;
			};
			data["Carrier"] = result["Carrier"];
			data["CarrierID"] = result["CarrierID"];
			data["bagOpDtl"] = result["bagOpDtl"];
			appearanceText = result["appearance"]; //外观
			integrityText = result["integrity"];  //完整性
			data["BagOperResultID"] = result["BagOperResultID"];
			bagoperationfrom.onSave(data, function(){
				//修改完成度和血袋属性
				var list = boutitemtable.cacheScandata[outformId];
				if (list && list.length == 1){
					list[0]["BloodBOutItem_HandoverCompletion"] = '3';
					list[0]["BloodBOutItem_BloodAppearance_BDict_CName"] = appearanceText;
					list[0]["BloodBOutItem_BloodIntegrity_BDict_CName"] = integrityText;
				};
				boutitemtable.SetItemColor(outitemId, result);	
			});
		});	   	
    };
    
    //血袋信息双击事件
	function onbagItemDbClick(event){
		var OutItemId = boutitemtable.getOutItemIdByElement(event.target);
		if (!OutItemId) {
			layer.msg("没有找到血袋发血明细编码", {time: 5000})
			return;
		};
        SavebagOper(OutItemId);
	};
	
	//血袋单击信息登记按钮事件
	function onRegisterClick(event){
		var ele = $(event.target);
		var OutItemId = ele.attr('outitemid'); //修改为发血明细ID,因为血袋号不唯一
		if (!OutItemId) {
			layer.msg("没有找到血袋发血明细编码", {time: 5000})
			return;
		};
	    SavebagOper(OutItemId);
	};
	
	//初始护工数据,护工其实也叫运送人
	function InitCarrier(){
		bagoperationfrom.initCarrier();
	};
	
	InitCarrier(); //初始护工数据
	onTableEvent(); //列表事件监听
	onFormEvent();  //表单事件监听
	boutformtable.cleardata(); //清除数据
 	patientInfotTable.render(); //获取病人信息

})
