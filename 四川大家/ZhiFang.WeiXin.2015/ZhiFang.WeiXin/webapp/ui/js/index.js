var shell_win_all = null;
$(function() {
	//页面所有功能对象
 	var shell_win = {
		/**系统*/
		system: {
			/**默认头像图片*/
			default_user_photo:Shell.util.Path.uiPath + "/img/icon/default_user_photo.png",
			/**微信ID*/
			open_id: Shell.util.Cookie.getCookie("openId"),
			/**用户ID*/
			user_id: Shell.util.Cookie.getCookie("userId"),
			/**系统初始化*/
			init: function() {
				
			}
		},
		/**缓存*/
		memory:{
			/**成员*/
			member: {
				/**没有成员*/
				error_no_data:"您好，您尚未维护就诊人信息，请返回，在“就诊人”中进行维护",
				/**是否已加载*/
				has_load: false,
				/**成员列表*/
				list: [],
				/**获取成员列表*/
				get_list: function(callback) {
					if (!shell_win.memory.member.has_load) {
						shell_win.memory.member.load(function(){
							if(callback){
								callback(shell_win.memory.member.list.concat());
							}
						});
						return;
					}
					
					if(callback){
						callback(shell_win.memory.member.list.concat());
						return;
					}else{
						return shell_win.memory.member.list.concat();
					}
				},
				/**加载数据*/
				load: function(callback) {
					ShellComponent.mask.loading();
					Shell.util.Server.ajax({
						url: Shell.util.Path.rootPath + 
						"/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetBSearchAccountVOListByWeiXinAccountId"
					}, function(data) {
						ShellComponent.mask.hide();
						if (data.success) {
							shell_win.memory.member.list = data.value || [];
							shell_win.memory.member.has_load = true;
							callback();
						} else {
							shell_win.memory.member.list = [];
							callback();
							ShellComponent.messagebox.msg(data.msg);
						}
					});
				},
				remove:function(member_id,report_id){
					//shell_win.memory.member.list
					var data = shell_win.memory.member.list,
						len = data.length,
						info = {};
					for(var i=0;i<len;i++){
						if(data[i].Id == member_id){
							info = data[i];
							break;
						}
					}
					var list = (info.RFIndexList || "").split(","),
						length = list.length;
						
					for(var i=0;i<length;i++){
						if(list[i] == report_id){
							list.splice(i,1);
							info.RFIndexList = list.join(",");
							break;
						}
					}
				}
			},
			/**账号*/
			account:{
				/**是否已加载*/
				has_load:false,
				/**账号信息*/
				info:{},
				/**获取账号信息*/
				get_info:function(callback){
					if (!shell_win.memory.account.has_load) {
						shell_win.memory.account.load(function(){
							if(callback){
								callback(shell_win.memory.account.info);
							}
						});
						return;
					}
					
					if(callback){
						callback(shell_win.memory.account.info);
						return;
					}else{
						return shell_win.memory.account.info;
					}
				},
				/**加载数据*/
				load:function(callback){
					ShellComponent.mask.loading();
					Shell.util.Server.ajax({
						url: Shell.util.Path.rootPath + 
						"/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountById?" + 
						"fields=LoginInputPasswordFlag&id=" + shell_win.system.user_id
					}, function(data) {
						ShellComponent.mask.hide();
						if (data.success) {
							shell_win.memory.account.info = data.value;
							shell_win.memory.account.has_load = true;
							callback();
						} else {
							shell_win.memory.account.info = {};
							callback();
							ShellComponent.messagebox.msg(data.msg);
						}
					});
				}
			}
		},
		/**首页*/
		home:{
			/**图标ID前缀*/
			icon_id:"home_icon_",
			/**首页初始化*/
			init:function(){
				var list = shell_win.home.get_icon_list(),
					len = list.length,
					html = [],
					num = 2;
				
//				html.push('<table class="home_icon_table">');
//				for(var i=0;i<len;i++){
//					if(i % num == 0){
//						if(i == 0){
//							html.push('<tr>');
//						}else{
//							html.push('</tr><tr>');
//						}
//					}
//					html.push('<td>');
//					html.push(shell_win.home.create_icon(list[i]));
//					html.push('</td>');
//				}
//				html.push('</table>');
				
				html.push('<div style="margin-top:30px;">');
				for(var i=0;i<len;i++){
					html.push(shell_win.home.create_icon(list[i]));
				}
				html.push('</div>');
				$("#page_home").html(html.join(""));
			},
			/**获取功能列表*/
			get_icon_list:function(){
				var list = [{
					id:"patient",
					name:"就诊人",
					icon:Shell.util.Path.uiPath + "/img/home/patient.png"
				},{
					id:"report",
					name:"报告查询",
					icon:Shell.util.Path.uiPath + "/img/home/report.png"
				},{
					id:"barcode",
					name:"扫一扫",
					icon:Shell.util.Path.uiPath + "/img/home/barcode.png"
				},{
					id:"account",
					name:"账号维护",
					icon:Shell.util.Path.uiPath + "/img/home/account.png"
				}];
				return list;
			},
			/**创建图标*/
			create_icon:function(info){
				
				if(!info || !info.id || !info.name) return null;
				
				var icon_id = shell_win.home.icon_id + info.id,
					html = [];
				
				html.push('<div id="' + icon_id + '" class="home_icon" on' + Shell.util.Event.touch + 
					'="shell_win_all.home.on_icon_touch(\'' + info.id + '\');">');
				html.push('<div class="home_icon_img_div"><img class="home_icon_img" src="' + info.icon + '"/></div>');
				html.push('<div class="home_icon_text">' + info.name + '</div>');
				html.push('</div>');
				
				return html.join("");
			},
			/**图标触摸处理*/
			on_icon_touch:function(id){
				shell_win[id].to_page();
			}
		},
		/**扫一扫*/
		barcode:{
			/**报告信息不存在提示*/
			error_no_data: "没有查到该报告信息",
			/**数据行id前缀*/
			grid_row_id:"barcode_grid_row_",
			/**最后一次触摸的数据航id*/
			last_row_id:null,
			user_photo_img_url:Shell.util.Path.uiPath + "/img/icon/user_photo.png",
			user_photo_img_touch_url:Shell.util.Path.uiPath + "/img/icon/user_photo_blue.png",
			barcode_but_img_url:Shell.util.Path.uiPath + "/img/icon/barcode_but_gray.png",
			barcode_but_img_touch_url:Shell.util.Path.uiPath + "/img/icon/barcode_but_blue.png",
			/**显示页面*/
			to_page:function(){
				shell_win.page.show(shell_win.barcode.get_html,"扫一扫",1);
			},
			/**获取页面*/
			get_html:function(callback){
				shell_win.memory.member.get_list(function(data){
					var len = data.length,
						photos = [],
						html = [];
					
					for(var i=0;i<len;i++){
						html.push('<div class="barcode_grid_row" id="');
						html.push(shell_win.barcode.grid_row_id);
						html.push(data[i].Id);
						html.push('" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.barcode.on_grid_row_touch(\'');
						html.push(data[i].Id);
						html.push('\',\'');
						html.push(data[i].Name);
						html.push('\')">');
						
						html.push('<div class="barcode_grid_row_photo_div">');
						html.push('<img id="');
						html.push(shell_win.barcode.grid_row_id);
						html.push(data[i].Id);
						html.push('_photo" src="');
						html.push(shell_win.barcode.user_photo_img_url);
						html.push('"/>');
						html.push('</div>');
						
						html.push('<div class="barcode_grid_row_name_div">');
						html.push(data[i].Name || "");
						html.push('</div>');
						
						html.push('<div class="barcode_grid_row_barcode_div">');
						html.push('<img id="');
						html.push(shell_win.barcode.grid_row_id);
						html.push(data[i].Id);
						html.push('_barcode" src="');
						html.push(shell_win.barcode.barcode_but_img_url);
						html.push('"/>');
						html.push('</div>');
						
						html.push('</div>');
					}
					
					if(len == 0){
						html.push('<div style="margin:20px 10px;text-align:center;">');
						html.push(shell_win.memory.member.error_no_data);
						html.push('</div>');
					}
					callback(html.join(""));
				});
			},
			/**头像触摸处理*/
			on_grid_row_touch:function(id,name){
				var is_touch = shell_win.event.is_touch();
				if(!is_touch) return;
				
				if(shell_win.barcode.last_row_id){
					$("#" + shell_win.barcode.grid_row_id + shell_win.barcode.last_row_id + "_photo")
						.attr("src",shell_win.barcode.user_photo_img_url);
					$("#" + shell_win.barcode.grid_row_id + shell_win.barcode.last_row_id + "_barcode")
						.attr("src",shell_win.barcode.barcode_but_img_url);
				}
				shell_win.barcode.last_row_id = id;
				$("#" + shell_win.barcode.grid_row_id + id + "_photo")
					.attr("src",shell_win.barcode.user_photo_img_touch_url);
				$("#" + shell_win.barcode.grid_row_id + id + "_barcode")
					.attr("src",shell_win.barcode.barcode_but_img_touch_url);
				
				shell_win.weixin.scan(function(value){
					var barcode = value.split(",").slice(-1);
					shell_win.barcode.to_report(barcode,name);
				});
			},
			/**查询报告*/
			to_report: function(bacode, name) {
				ShellComponent.mask.loading();
				Shell.util.Server.ajax({
					url: Shell.util.Path.rootPath + 
					"/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetBScanningBarCodeReportFormListByBarcodeSearchUserName" + 
					"?Barcode=" + bacode + "&SearchUserName=" + name
				}, function(data) {
					var html = [];
					if (data.success) {
						var list = data.value || [],
							len = list.length;

						if (len == 0) {
							html.push('<div style="text-align:center;padding:5px;font-weight:bolder;">');
							html.push(shell_win.barcode.error_no_data);
							html.push('</div>');
						} else {
							for (var i = 0; i < len; i++) {
								html.push(shell_win.report.get_report_info_html(list[i]));
							}
						}
					} else {
						html.push('<div style="text-align:center;padding:5px;font-weight:bolder;">');
						html.push(data.msg);
						html.push('</div>');
					}
					ShellComponent.mask.hide();
					shell_win.page.show(html.join(""), "报告信息",2);
				})
			}
		},
		/**报告查询*/
		report:{
			/**头像id前缀*/
			user_photo_id:"report_member_photo_",
			user_photo_img_url:Shell.util.Path.uiPath + "/img/icon/user_photo_a.png",
			user_photo_img_touch_url:Shell.util.Path.uiPath + "/img/icon/user_photo_a_blue.png",
			/**最后一次触摸的成员ID*/
			last_member_id:null,
			/**最后一次触摸的成员姓名*/
			last_member_name:null,
			/**最后一次触摸的报告列表行*/
			last_report_id:null,
			/**当前页签*/
			page:1,
			/**每页数量*/
			limit:20,
			/**缓存列表数据*/
			list:[],
			/**对象属性映射*/
			obj: {
				/**属性字段*/
				info_field: "info",
				/**属性内容*/
				info_obj: {
					/**记录ID*/
					id: "id",
					/**报告ID*/
					report_id: "ReportId",
					/**报告状态*/
					report_status: "status",
					/**医院图片*/
					hospital_img: "hospital_img",
					/**医院名称*/
					hospital_name: "HospitalName",
					/**项目名称*/
					item_list_name: "item_list_name",
					/**检验单号*/
					check_list_number: "CheckListNumber",
					/**患者姓名*/
					patient_id: "PatientId",
					/**患者姓名*/
					patient_name: "PatientName",
					/**就诊时间*/
					visit_time: "VisitTime",
					/**报告时间*/
					report_time: "ReportTime",
					/**报告类型*/
					report_type:"ReportType",
					/**备注*/
					remarks:"ReportFORMMEMO"
				},
				/**生化物项目列表字段*/
				item_list_field: "list",
				/**生化物项目对象内容*/
				item_obj: {
					/**名称*/
					name: "ItemsName",
					/**结果*/
					result: "Result",
					/**结果状态*/
					status:"ResultStatus",
					/**单位*/
					unit: "Unit",
					/**参考值*/
					reference_value: "ReferenceValue",
					shortcode: "ShortCode"
				},
				/**结果状态*/
				status:{
					"HH":{count:2,value:"超高",url:Shell.util.Path.uiPath + "/img/icon/up_red.png"},
					"H":{count:1,value:"偏高",url:Shell.util.Path.uiPath + "/img/icon/up_red.png"},
					"L":{count:1,value:"偏低",url:Shell.util.Path.uiPath + "/img/icon/down_red.png"},
					"LL":{count:2,value:"超低",url:Shell.util.Path.uiPath + "/img/icon/down_red.png"}
				},
				/**微生物项目列表字段*/
				micro_item_list_field:"MicroItemlist",
				/**微生物项目对象*/
				micro_item_obj:{
					/**项目名称*/
					name:"MicroItemName",
					MicroItemNo:"MicroItemNo",
					/**项目说明*/
					desc:"MicroItemDesc",
					/**过程描述数组*/
					DescList:"DescList",
					/**细菌列表字段*/
					micro_list_field:"MicroList",
					/**细菌对象内容*/
					micro_obj:{
						/**名称*/
						name:"MicroName",
						MicroNo:"MicroNo",
						/**备注*/
						remarks:"MicroDesc",
						/**药敏结果列表字段*/
						sensitivity_list_field:"AnitList",
						/**药敏结果对象内容*/
						sensitivity_obj:{
							/**药物名称*/
							name:"AnitName",
							/**类型*/
							type:"MethodName",
							/**结果值*/
							value:"SusQuan",
							/**药敏度*/
							result:"Suscept",
							RefRange:"RefRange"
						}
					}
				}
			},
			
			/**显示页面*/
			to_page:function(){
				shell_win.page.show(shell_win.report.get_html,"报告查询",1);
			},
			/**获取页面*/
			get_html:function(callback){
				shell_win.memory.member.get_list(function(data){
					var len = data.length,
						photos = [],
						html = [];
					
					for(var i=0;i<len;i++){
						photos.push('<div class="user_photo" id="');
						photos.push(shell_win.report.user_photo_id);
						photos.push(data[i].Id);
						photos.push('" on');
						photos.push(Shell.util.Event.touch);
						photos.push('="shell_win_all.report.on_member_photo_touch(\'');
						photos.push(data[i].Id);
						photos.push('\',\'');
						photos.push(data[i].Name);
						photos.push('\')">');
						
						photos.push('<img id="');
						photos.push(shell_win.report.user_photo_id);
						photos.push(data[i].Id);
						photos.push('_img" src="');
						photos.push(shell_win.report.user_photo_img_url);
						photos.push('"/>');
						
						photos.push('<span>');
						photos.push(data[i].Name || "");
						photos.push('</span>');
						photos.push('</div>');
					}
					
					if(len > 0){
						html.push('<div class="user_photo_div">');
						html.push(photos.join(""));
						html.push('</div>');
		
						html.push('<div class="report_grid_view" id="report_grid_view" hidden="hidden"></div>');
					}else{
						html.push('<div style="margin:20px 10px;text-align:center;">');
						html.push(shell_win.memory.member.error_no_data);
						html.push('</div>');
					}
					callback(html.join(""));
				});
			},
			/**头像触摸处理*/
			on_member_photo_touch:function(id,name){
				if(shell_win.report.last_member_id){
					$("#" + shell_win.report.user_photo_id + shell_win.report.last_member_id).removeClass("user_photo_touch");
					$("#" + shell_win.report.user_photo_id + shell_win.report.last_member_id + "_img")
						.attr("src",shell_win.report.user_photo_img_url);
				}
				shell_win.report.last_member_id = id;
				
				$("#" + shell_win.report.user_photo_id + id).addClass("user_photo_touch");
				$("#" + shell_win.report.user_photo_id + id + "_img")
					.attr("src",shell_win.report.user_photo_img_touch_url);
					
				shell_win.report.last_member_name = name;
				shell_win.report.page = 1;//初始化到第一页
				shell_win.report.get_report_list(id,name,function(list){
					var html = shell_win.report.get_grid_html(list);
					
					if(!html){
						html = '<div class="report_grid_row" style="text-align:center;">没有报告</div>';
					}
						
					$("#report_grid_view").html(html);
					$("#report_grid_view").show();
				});
			},
			/**加载更多数据*/
			on_load_more:function(){
				var id = shell_win.report.last_member_id,
					name = shell_win.report.last_member_name;
					
				shell_win.report.page++;//加载下一页数据
				shell_win.report.get_report_list(id,name,function(list){
					var html = shell_win.report.get_grid_html(list);
					
					$("#report_row_load_more").remove();
					if(html.length > 0){
						$("#report_grid_view").append(html);
					}
				});
			},
			/**获取列表内容*/
			get_grid_html:function(list){
				var len = list.length,
					html = [];
				
				for(var i=0;i<len;i++){
					html.push('<div class="report_grid_row" id="report_row_');
					html.push(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.report_id]);
					html.push('" on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.report.on_report_row_touch(\'');
					html.push(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.report_id]);
					html.push('\')">');
					
					html.push('<div class="report_grid_row_name">');
					html.push(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.hospital_name]);
					html.push('</div>');
					
					html.push('<div class="report_grid_row_right">〉</div>');
					
					html.push('<div class="report_grid_row_date">');
					html.push(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.visit_time]);
					html.push('</div>');
					
					html.push('</div>');
				}
					
				if(len == shell_win.report.limit){
					html.push('<div class="report_grid_row" id="report_row_load_more" style="text-align:center;" on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.report.on_load_more()">');
					html.push('加载更多</div>');
				}
				return html.join("");
			},
			/**是否已阅读*/
			has_read:function(id){
				var data = shell_win.memory.member.list,
					len = data.length,
					info = {};
				for(var i=0;i<len;i++){
					if(data[i].Id == shell_win.report.last_member_id){
						info = data[i];
						break;
					}
				}
				var list = (info.RFIndexList || "").split(","),
					length = list.length;
					
				for(var i=0;i<length;i++){
					if(list[i] == id) return false;
				}
				
				return true;
			},
			
			/**获取报告列表*/
			get_report_list:function(id,name,callback){
				ShellComponent.mask.loading();
				Shell.util.Server.ajax({
					url:Shell.util.Path.rootPath + 
					"/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetBSearchAccountRFList" + 
					"?SearchAccountId=" +id + "&Name=" + name + 
					'&Page=' + shell_win.report.page + '&limit=' + shell_win.report.limit
				},function(data){
					ShellComponent.mask.hide();
					var html = "";
					if(shell_win.report.list == 1){
						shell_win.report.list = [];
					}
					if(data.success){
						var list = data.value || [],
							len =list.length;
						for(var i=0;i<len;i++){
							list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.patient_id] = id;
						}
						
						shell_win.report.list = shell_win.report.list.concat(list);
						
						callback(list);
					}else{
						callback([]);
						ShellComponent.messagebox.msg(data.msg);
					}
				});
			},
			/**报告列表行触摸处理*/
			on_report_row_touch:function(id){
				var is_touch = shell_win.event.is_touch();
				if(!is_touch) return;
				
				if(shell_win.report.last_report_id){
					$("#report_row_" + shell_win.report.last_report_id).removeClass("button_touch");
				}
				shell_win.report.last_report_id = id;
				$("#report_row_" + id).addClass("button_touch");
				
				var list = shell_win.report.list,
					len = list.length,
					info = null;
					
				for(var i=0;i<len;i++){
					if(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.report_id] == id){
						info = list[i];
						break;
					}
				}
				//加载报告信息
				shell_win.report.get_report_info(
					info[shell_win.report.obj.info_field][shell_win.report.obj.info_obj.patient_id],
					id,
					function(data){
						var html = [];
						for(var i=0;i<data.length;i++){
							html.push(shell_win.report.get_report_info_html(data[i]));
						}
						shell_win.page.show(html.join(""), "报告信息",2);
						
						var has_read = shell_win.report.has_read(id);
						if(!has_read){
							shell_win.report.report_load(list[i]);
						}
					}
				);
			},
			/**报告已读处理*/
			report_load:function(info){
				var patient_id = info[shell_win.report.obj.info_field][shell_win.report.obj.info_obj.patient_id];
				
				var list = shell_win.memory.member.list,
					len = list.length;
					
				for(var i=0;i<len;i++){
					if(list[i].Id == patient_id){
						if(list[i].UnReadRFCount > 0) list[i].UnReadRFCount--;
						if(list[i].UnReadRFCount == 0){
							$("#report_member_li_" + patient_id + "_badge").remove();
						}else{
							$("#report_member_li_" + patient_id + "_badge").html(list[i].UnReadRFCount);
						}
						$("#report_li_status_" + info[shell_win.report.obj.info_field][shell_win.report.obj.info_obj.report_id]).remove();
						
						shell_win.memory.member.remove(
							info[shell_win.report.obj.info_field][shell_win.report.obj.info_obj.patient_id],
							info[shell_win.report.obj.info_field][shell_win.report.obj.info_obj.report_id]
						);
						$("#report_member_li_has_read_mark_" + info[shell_win.report.obj.info_field][shell_win.report.obj.info_obj.report_id]).remove();
						break;
					}
				}
			},
			/**获取报告信息*/
			get_report_info:function(member_id,report_id,callback){
				ShellComponent.mask.loading();
				Shell.util.Server.ajax({
					url:Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetSearchAccountReportFormById?" + 
						"ReportFormIndexId=" + report_id + "&SearchAccountId=" + member_id
//					url:Shell.util.Path.uiPath + "/file/report.txt"
				},function(data){
					ShellComponent.mask.hide();
					if(data.success){
						callback(data.value);
					}else{
						ShellComponent.messagebox.msg(data.msg);
					}
				});
			},
			/**获取报告信息页面内容*/
			get_report_info_html: function(data) {
				var obj = shell_win.report.obj;
					value = data || {},
					report_type = value[obj.info_field][obj.info_obj.report_type],
					html = 
						'<div style="margin:20px 10px;text-align:center;">' + 
							'不存在模板！报告类型：' + report_type + 
						'</div>';
					
				switch(report_type){
					case "Normal" : //生化类
					case "NormalIncImage" : //生化类（图）
						html = shell_win.report.normal_model.get_html(data);
						break;
						
					case "Micro" : //微生物
					case "MicroIncImage" : //微生物（图）
						html = shell_win.report.micro_model.get_html(data);
						break;
						
					case "CellMorphology" : //细胞形态学Marrow
					case "FishCheck" : //Fish检测（图）Marrow
					case "SensorCheck" : //院感检测（图）
					case "ChromosomeCheck" : //染色体检测（图）Marrow
					case "PathologyCheck" : //病理检测（图）Marrow
						break;
				}
					
				return html;
			},
			/**生化类报告模板*/
			normal_model:{
				/**获取内容*/
				get_html:function(data){
					var obj = shell_win.report.obj,
						value = data || {},
						info = value[obj.info_field] || {},
						has_status = false,
						div = [];
					
					//报告内容
					div.push('<div class="report_info_div">');
					//出单医院
					div.push('<div class="report_info_title_div">');
					div.push(info[obj.info_obj.hospital_name]);
					div.push('</div>');
					
					//检验单号
					div.push('<div class="report_info_data_div">检验单号：');
					div.push(info[obj.info_obj.check_list_number]);
					div.push('</div>');
					//患者姓名
					div.push('<div class="report_info_data_div">患者姓名：');
					div.push(info[obj.info_obj.patient_name]);
					div.push('</div>');
					//就诊时间
					div.push('<div class="report_info_data_div">就诊时间：');
					div.push(info[obj.info_obj.visit_time]);
					div.push('</div>');
					//报告时间
					div.push('<div class="report_info_data_div">报告时间：');
					div.push(info[obj.info_obj.report_time]);
					div.push('</div>');
					
					//原始报告
//					div.push('<div class="report_info_data_div">原始报告：');
//					div.push('<button onclick="shell_win_all.report.show_resouce_report_info();">原始报告</button>');
//					div.push('</div>');
					
					div.push('</div>');
					
					//项目内容列表
					var list = value[obj.item_list_field],
						len = list.length,
						list_table = [];
	
					list_table.push('<table class="report_items_table">');
					list_table.push('<thead><th>检验项目</th><th>结果</th><th>单位</th><th>参考值</th></thead>');
					list_table.push('<tbody>');
					for (var i = 0; i < len; i++) {
						var row = list[i];
						//list_table.push('<tr>');
						
						list_table.push('<tr on');
						list_table.push(Shell.util.Event.touch);
						list_table.push('="shell_win_all.report.show_clinical_significance(\'');
						list_table.push(row[obj.item_obj.shortcode]);
						list_table.push('\')">');
						
						list_table.push('<td>');
						list_table.push(row[obj.item_obj.name]);
						list_table.push('</td>');
						list_table.push('<td>');
						list_table.push(row[obj.item_obj.result]);
						
						var status = row[obj.item_obj.status];
						if(status){
							has_status = true;
							var status_obj = obj.status[status];
							for(var m=0;m<status_obj.count;m++){
								list_table.push('<img style="width:10px;height:10px;margin-top:-2px;" src="');
								list_table.push(status_obj.url);
								list_table.push('"/>');
							}
						}
						
						list_table.push('</td>');
						list_table.push('<td>');
						list_table.push(row[obj.item_obj.unit]);
						list_table.push('</td>');
						list_table.push('<td>');
						list_table.push(row[obj.item_obj.reference_value]);
						list_table.push('</td>');
						list_table.push('</tr>');
					}
					list_table.push('</tbody>');
					list_table.push('</table>');
	
					div.push(list_table.join(""));
					
					//存在偏高偏低状态时显示备注信息
					if(has_status){
						var status = obj.status;
						div.push('<div class="report_info_note_div">');
						//第一行：偏高+超高
						div.push('<div style="font-size:0.8em;">备注:&nbsp;');
						div.push(status.H.value);
						div.push(':');
						div.push('<img style="width:10px;height:10px;margin-right:10px;margin-top:-2px;" src="');
						div.push(status.H.url);
						div.push('"/>');
						
						div.push(status.HH.value);
						div.push(':');
						div.push('<img style="width:10px;height:10px;margin-top:-2px;" src="');
						div.push(status.HH.url);
						div.push('"/>');
						div.push('<img style="width:10px;height:10px;margin-right:10px;margin-top:-2px;" src="');
						div.push(status.HH.url);
						div.push('"/>');
		//				div.push('</div>');
		//				
		//				//第二行：偏低+超低
		//				div.push('<div style="style="font-size:0.8em;">');
						div.push(status.L.value);
						div.push(':');
						div.push('<img style="width:10px;height:10px;margin-right:10px;margin-top:-2px;" src="');
						div.push(status.L.url);
						div.push('"/>');
						
						div.push(status.LL.value);
						div.push(':');
						div.push('<img style="width:10px;height:10px;margin-top:-2px;" src="');
						div.push(status.LL.url);
						div.push('"/>');
						div.push('<img style="width:10px;height:10px;margin-top:-2px;" src="');
						div.push(status.LL.url);
						div.push('"/>');
					}
						
					div.push('</div>');
	
					return div.join("");
				}
			},
			/**微生物报告模板*/
			micro_model:{
				/**获取内容*/
				get_html:function(data){
					var value = data || {},
						div = [];
						
					//报告信息内容
					div.push(this.get_info(value));
					//项目报告列表内容
					div.push(this.get_items(value));
					
					return div.join("");
				},
				/**报告信息内容*/
				get_info:function(value){
					var obj = shell_win.report.obj,
						info = value[obj.info_field] || {},
						div = [];
						
					//报告内容
					div.push('<div class="report_info_div">');
					
					//出单医院
					div.push('<div class="report_info_title_div">');
					div.push(info[obj.info_obj.hospital_name]);
					div.push('</div>');
					
					//检验单号
					div.push('<div class="report_info_data_div">检验单号：');
					div.push(info[obj.info_obj.check_list_number]);
					div.push('</div>');
					//患者姓名
					div.push('<div class="report_info_data_div">患者姓名：');
					div.push(info[obj.info_obj.patient_name]);
					div.push('</div>');
					//报告时间
					div.push('<div class="report_info_data_div">报告时间：');
					div.push(info[obj.info_obj.report_time]);
					div.push('</div>');
					
					//原始报告
//					div.push('<div class="report_info_data_div">原始报告：');
//					div.push('<button onclick="shell_win_all.report.show_resouce_report_info();">原始报告</button>');
//					div.push('</div>');
					
					div.push('</div>');
					
					return div.join("");
				},
				/**获取项目列表内容*/
				get_items:function(value){
					var obj = shell_win.report.obj,
						micro_item_obj = obj.micro_item_obj || {},
						list = value[obj.micro_item_list_field] || [],
						len = list.length,
						div = [];
	
					for(var i=0;i<len;i++){
						var item = list[i];
						//项目名称
						div.push('<div class="report_item_name_div">');
						div.push(item[micro_item_obj.name]);
//						div.push("&nbsp;");
//						div.push(item[micro_item_obj.desc]);
						div.push('</div>');
						//微生物列表
						div.push('<div class="report_item_content_div">');
						//描述
						div.push(this.get_desc(item[micro_item_obj.desc]));
						div.push(this.get_desc_list(item[micro_item_obj.DescList]));
						div.push(this.get_micro_list(item[micro_item_obj.micro_list_field]) || 
							item[micro_item_obj.micro_obj.remarks]);
						div.push('</div>');
					}
					
					return div.join("");
				},
				/**获取描述*/
				get_desc:function(value){
					var div = [];
					
					if(value){
						div.push('<div class="report_item_micro_info_div" style="background:orange;color:#ffffff;">');
						div.push(value);
						div.push('</div>');
					}
					
					return div.join("");
				},
				/**获取过程描述列表*/
				get_desc_list:function(list){
					var arr = list || [],
						len = arr.length,
						div = [];
						
					for(var i=0;i<len;i++){
						div.push('<div class="report_item_micro_info_div">');
						div.push(arr[i]);
						div.push('</div>');
					}
						
					return div.join("");
				},
				/**获取微生物列表内容*/
				get_micro_list:function(list){
					var obj = shell_win.report.obj,
						micro_obj = obj.micro_item_obj.micro_obj || {},
						list = list || [],
						len = list.length,
						div = [];
						
					for(var i=0;i<len;i++){
						var micro = list[i],
							sensitivity_list = micro[micro_obj.sensitivity_list_field] || [],
							sensitivity_exp = sensitivity_list.length == 0 ? "阴性" : "阳性";
						
						div.push('<div class="report_item_micro_info_div">');
						//微生物名称
						div.push('<div class="report_item_micro_info_title_div">');
						div.push(micro[micro_obj.name]);
						div.push('</div>');
//						//阴性/阳性
//						div.push('<span style="margin:0 10px;">');
//						div.push(sensitivity_exp);
//						div.push('</span>');
						//药敏结果列表
						div.push('<div class="report_item_micro_sensitivity_div">');
						//描述
						div.push(this.get_desc(micro[micro_obj.remarks]));
						div.push(this.get_sensitivity_list(sensitivity_list));
						div.push('</div>');
					}
					
					return div.join("");
				},
				/**获取药敏结果列表内容*/
				get_sensitivity_list:function(list){
					var obj = shell_win.report.obj,
						sensitivity_obj = obj.micro_item_obj.micro_obj.sensitivity_obj || {},
						list = list || [],
						len = list.length,
						div = [];
					
					div.push('<table class="report_item_micro_sensitivity_table">');
					div.push('<thead><th>药物(抗生素)</th><th>结果</th><th>药敏度</th></thead>');
					div.push('<tbody>');
					
					for(var i=0;i<len;i++){
						var row = list[i];
						div.push('<tr>');
						
						div.push('<td>');
						div.push(row[sensitivity_obj.name]);
						div.push('</td>');
						
						div.push('<td>');
						div.push(row[sensitivity_obj.value]);
						div.push(' (');
						div.push(row[sensitivity_obj.type]);
						div.push(')');
						div.push('</td>');
						
						div.push('<td>');
						div.push(row[sensitivity_obj.result]);
						div.push('</td>');
						
						div.push('</tr>');
					}
					
					div.push('</tbody></table>');
					
					return div.join("");
				}
			},
			/**查看原始报告单*/
			show_resouce_report_info:function(){
				var url = Shell.util.Path.uiPath + "/file/test.pdf";
				var embed = 
					'<iframe src="' + url + '" frameborder="0" ' +
						'style="overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;' +
						'position:absolute;top:50px;left:0px;right:0px;bottom:0px"' +
					'></iframe>'
				
				var html = embed;
				shell_win.page.show(html,"原始报告",3);
			},
			/**查看临床意义*/
			show_clinical_significance:function(code){
				var is_touch = shell_win.event.is_touch();
				if(!is_touch) return;
				
				var url = Shell.util.Path.rootPath + "/TestItemClinicalSignificance.aspx?ItemShortCode=" + code;
				var embed = 
					'<iframe src="' + url + '" frameborder="0" ' +
						'style="overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;' +
						'position:absolute;top:50px;left:0px;right:0px;bottom:0px"' +
					'></iframe>'
				
				var html = embed;
				shell_win.page.show(html,"项目临床意义",3);
			}
		},
		/**就诊人*/
		patient:{
			/**数据行id前缀*/
			grid_row_id:"patient_grid_row_",
			user_photo_img_url:Shell.util.Path.uiPath + "/img/icon/user_photo.png",
			user_photo_img_touch_url:Shell.util.Path.uiPath + "/img/icon/user_photo_blue.png",
			add_button_url:Shell.util.Path.uiPath + "/img/icon/add_a_blue.png",
			edit_button_url:Shell.util.Path.uiPath + "/img/icon/minus_red_32.png",
			/**最后一次触摸的就诊人列表行*/
			last_patient_id:null,
			/***/
			last_patient_hospital_row:null,
			/**显示页面*/
			to_page:function(){
				shell_win.page.show(shell_win.patient.get_html,"就诊人维护",1);
			},
			/**获取页面*/
			get_html2:function(callback){
				shell_win.memory.member.get_list(function(data){
					var len = data.length,
						html = [];
					
					html.push('<div>');
					for(var i=0;i<len;i++){
						html.push('<div class="barcode_grid_row" id="');
						html.push(shell_win.patient.grid_row_id);
						html.push(data[i].Id);
						html.push('" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.patient.on_row_touch(\'');
						html.push(data[i].Id);
						html.push('\')">');
						
						html.push('<div class="barcode_grid_row_photo_div">');
						html.push('<img id="');
						html.push(shell_win.patient.grid_row_id);
						html.push(data[i].Id);
						html.push('_img" src="');
						html.push(shell_win.patient.user_photo_img_url);
						html.push('"/>');
						html.push('</div>');
						
						html.push('<div class="barcode_grid_row_name_div">');
						html.push(data[i].Name || "");
						html.push('</div>');
						
						//删除DIV
						html.push('<div style="float:right;margin:10px" hidden="hidden" id="');
						html.push(shell_win.patient.grid_row_id);
						html.push(data[i].Id);
						html.push('_del">');
						html.push('<button class="btn btn-xs btn-danger" style="padding:5px 10px;" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.patient.del(\'');
						html.push(data[i].Id);
						html.push('\');">');
						html.push('<i class="glyphicon glyphicon-trash"></i>');
						html.push('</button>');
						html.push('</div>');
						
						html.push('</div>');
					}
					
					html.push('<div class="barcode_grid_row" id="');
					html.push(shell_win.patient.grid_row_id);
					html.push('0" on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.patient.on_row_touch(\'0\')">');
					
					html.push('<div class="barcode_grid_row_photo_div">');
					html.push('<img style="width:36px;height:36px;padding:5px;margin:2px;border:1px solid #169ADA;border-radius:5px;" src="');
					html.push(shell_win.patient.add_button_url);
					html.push('"/>');
					html.push('</div>');
					
					html.push('<div class="barcode_grid_row_name_div">');
					html.push("新增就诊人");
					html.push('</div>');
					
					html.push('</div>');
					
					callback(html.join(""));
				});
			},
			get_html:function(callback){
				shell_win.memory.member.get_list(function(data){
					var len = data.length,
						html = [];
					
					html.push('<div>');
					
					for(var i=0;i<len;i++){
						html.push('<div class="barcode_grid_row" id="');
						html.push(shell_win.patient.grid_row_id);
						html.push(data[i].Id);
						html.push('" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.patient.on_row_touch(\'');
						html.push(data[i].Id);
						html.push('\')">');
						
						html.push('<div class="barcode_grid_row_photo_div">');
						html.push('<img id="');
						html.push(shell_win.patient.grid_row_id);
						html.push(data[i].Id);
						html.push('_img" src="');
						html.push(shell_win.patient.user_photo_img_url);
						html.push('"/>');
						html.push('</div>');
						
						html.push('<div class="barcode_grid_row_name_div">');
						html.push(data[i].Name || "");
						html.push('</div>');
						
						//删除DIV
						html.push('<div name="patient_row_del_div" class="del_mask_div" style="border-radius:5px;" hidden="hidden">');
						html.push('<img src="');
						html.push(Shell.util.Path.uiPath);
						html.push('/img/icon/fork_red_32.png" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.patient.del(\'');
						html.push(data[i].Id);
						html.push('\');"/>');
						html.push('</div>');
						
						html.push('</div>');
					}
					
					html.push('<div>');
					
					html.push('<div class="barcode_grid_row" style="float:left;width:45%;" on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.patient.on_touch_add_patient();">');
					html.push('<div class="barcode_grid_row_photo_div">');
					html.push('<img style="width:32px;height:32px;margin:4px;" src="');
					html.push(shell_win.patient.add_button_url);
					html.push('"/>');
					html.push('</div>');
					html.push('<div class="barcode_grid_row_name_div" style="margin-left:0">');
					html.push("新增就诊人");
					html.push('</div>');
					html.push('</div>');
					
					html.push('<div class="barcode_grid_row" style="float:right;width:45%;"  on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.patient.touch_edit_button(this);">');
					html.push('<div class="barcode_grid_row_photo_div">');
					html.push('<img id="patient_action_edit_but" style="width:32px;height:32px;margin:4px;" src="');
					html.push(shell_win.patient.edit_button_url);
					html.push('"/>');
					
					html.push('</div>');
					html.push('<div class="barcode_grid_row_name_div" style="margin-left:0">');
					html.push("删除就诊人");
					html.push('</div>');
					html.push('</div>');
					
					html.push('</div>');
					
					callback(html.join(""));
				});
			},
			/**就诊人编辑按钮*/
			touch_edit_button:function(){
				var com = $("#patient_action_edit_but"),
					divs = $("div[name='patient_row_del_div']"),
					is_touched = $(com).attr("is_touched");
					
				if(is_touched == "true"){
					$(com).attr("is_touched","false")
					$(com).removeClass("patient_hospital_title_div_edit_but_touch");
					divs.hide();
				}else{
					$(com).attr("is_touched","true")
					$(com).addClass("patient_hospital_title_div_edit_but_touch");
					divs.show();
				}
			},
			
			/**隐藏编辑DIV*/
			hide_edit_divs:function(){
				var com = $("#patient_action_edit_but"),
					divs = $("div[name='patient_row_del_div']"),
					is_touched = $(com).attr("is_touched");
				
				if(is_touched == "true"){
					$(com).attr("is_touched","false")
					$(com).removeClass("patient_hospital_title_div_edit_but_touch");
					divs.hide();
				}
			},
			/**新增成员*/
			on_touch_add_patient:function(){
				shell_win.patient.hide_edit_divs();
				var html = shell_win.patient.get_info_page_add();
				shell_win.page.show(html, "就诊人信息维护", 2);
			},
			/**数据行触摸处理*/
			on_row_touch:function(id){
				var com = $("#patient_action_edit_but"),
					is_touched = $(com).attr("is_touched");
				if(is_touched == "true") return;
				
				var is_touch = shell_win.event.is_touch();
				if(!is_touch) return;
				
				if(shell_win.patient.last_patient_id && shell_win.patient.last_patient_id != "0"){
					$("#" + shell_win.patient.grid_row_id + shell_win.patient.last_patient_id + "_img")
						.attr("src",shell_win.patient.user_photo_img_url);
				}
				
				if (id) { //修改
					var move_x = shell_win.event.move_x();
					if(!move_x){
						if(shell_win.patient.last_patient_id){
							$("#" + shell_win.patient.grid_row_id + shell_win.patient.last_patient_id + "_del").hide();
						}
						$("#" + shell_win.patient.grid_row_id + id + "_img")
							.attr("src",shell_win.patient.user_photo_img_touch_url);
							
						shell_win.patient.get_info_page_edit(id,function(html){
							shell_win.page.show(html, "就诊人信息维护", 2);
						});
					}else if(move_x > 0){
						$("#" + shell_win.patient.grid_row_id + id + "_del").hide();
					}else if(move_x < 0){
						if(shell_win.patient.last_patient_id){
							$("#" + shell_win.patient.grid_row_id + shell_win.patient.last_patient_id + "_del").hide();
						}
						$("#" + shell_win.patient.grid_row_id + id + "_del").show();
					}
				}
				shell_win.patient.last_patient_id = id;
			},
			/**获取就诊人信息页面-新增*/
			get_info_page_add: function() {
				var member = shell_win.patient.get_info_member(),
					hospital = shell_win.patient.get_info_hospital(),
					html = shell_win.patient.get_info_page_html(member,hospital);

				return html;
			},
			/**获取就诊人信息页面-修改*/
			get_info_page_edit: function(id,callback) {
				shell_win.patient.get_info_from_server(id,function(data){
					var member = shell_win.patient.get_info_member(data),
						hospital = shell_win.patient.get_info_hospital(data),
						html = shell_win.patient.get_info_page_html(member,hospital);
					
					callback(html);
				});
			},
			/**获取就诊人信息页面内容*/
			get_info_page_html:function(member,hospital){
				var html = 
				'<div style="padding:5px auto;">' + 
					member + hospital +
					'<div class="shell_form_button_div" style="margin:50px 20%;">' +
					'<button id="patient_info_submit_btn" on' +
					Shell.util.Event.touch + '="shell_win_all.patient.submit();">保存</button></div>' + 
				'</div>';

				return html;
			},
			/**获取就诊人信息内容*/
			get_info_member:function(info){
				var data = info || {};
				var html =
					'<input type="hidden" id="patient_info_id" value="' + (data.Id || '') + '"></input>' +
					ShellComponent.create_input({
						id: "patient_info_name",
						icon: Shell.util.Path.uiPath + "/img/icon/user_photo_small.png",
						placeholder: "姓名",
						value:data.Name || ""
					}) + 
					ShellComponent.create_input({
						id: "patient_info_mobilecode",
						icon: Shell.util.Path.uiPath + "/img/icon/phone_small.png",
						placeholder: "手机号",
						value:data.MobileCode || ""
					});
					
				return html;
			},
			/**获取就诊人医院信息内容*/
			get_info_hospital:function(info){
				var data = info || {},
					list = data.SearchList || [],
					len = list.length,
					html = [];
					
				html.push(
					'<div class="patient_hospital_title_div">' +
						'<span>就诊医院信息<span>' +
						'<button class="patient_hospital_title_div_add_but" on' + 
						Shell.util.Event.touch +
						'="shell_win_all.patient.add_hospital_info();"></button>' +
						'<button class="patient_hospital_title_div_edit_but" on' + 
						Shell.util.Event.touch +
						'="shell_win_all.patient.touch_hospital_edit_button(this);"></button>' +
					'</div>'
				);
				
				html.push('<div id="patient_info_hospital_list">');
				
				
				var arr = [];
				//是否已经存在在数值	
				var has_value = function(value){
					var length = arr.length,
						bo = false;
					for(var i=0;i<length;i++){
						if(arr[i].FieldsValue == value){
							bo = true;break;
						}
					}
					return bo;
				};
				
				for(var i=0;i<len;i++){
					var code = list[i].FieldsCode;
					if(code == "MobileCode") continue;
					
					var value = list[i].FieldsValue;
					if(!has_value(value)){
						arr.push(list[i]);
					}
				}
				
				for(var i in arr){
					html.push(shell_win.patient.get_hospital_row(arr[i]));
				}
				
				html.push('</div>');
					
				return html.join("");
			},
			/**触摸编辑按钮*/
			touch_hospital_edit_button:function(com){
				var divs = $("div[name='patient_hospital_row_del_div']");
				var is_touched = $(com).attr("is_touched");
				if(is_touched == "true"){
					$(com).attr("is_touched","false")
					$(com).removeClass("patient_hospital_title_div_edit_but_touch");
					divs.hide();
				}else{
					$(com).attr("is_touched","true")
					$(com).addClass("patient_hospital_title_div_edit_but_touch");
					divs.show();
				}
			},
			/**新增医院信息内容*/
			add_hospital_info:function(info){
				var div = $("#patient_info_hospital_list");
				var html = shell_win.patient.get_hospital_row();
				div.append(html);
			},
			/**获取一行医院信息*/
			get_hospital_row:function(info){
				var data =info|| {};
				
				var html = [];
				
				html.push('<div class="patient_hospital_row" name="patient_info_hospital_list_row" on');
				html.push(Shell.util.Event.touch);
				html.push('="shell_win_all.patient.on_touch_hospital_info(this);">');
				
				html.push('<div class="shell_form_input_div" style="width:49%;float:left;margin-left:1px;">');
				html.push('<input placeholder="病历号/就诊卡" value="');
				html.push(data.FieldsValue || '');
				html.push('"></div>');
				
				html.push('<div class="shell_form_input_div" style="width:49%;float:right;margin-right:1px;">');
				html.push('<input placeholder="就诊医院" value="');
				html.push(data.Comment || '');
				html.push('"></div>');
				
				html.push('<div name="patient_hospital_row_del_div" class="del_mask_div" hidden="hidden">');
				html.push('<img src="');
				html.push(Shell.util.Path.uiPath);
				html.push('/img/icon/fork_red_32.png" on');
				html.push(Shell.util.Event.touch);
				html.push('="shell_win_all.patient.del_hospital_info(this);"/>');
				html.push('</div>');
				
				html.push('</div>');
				
				return html.join("");
			},
			/**医院信息行触摸处理*/
			on_touch_hospital_info:function(com){
				var is_touch = shell_win.event.is_touch();
				if(!is_touch) return;
				
				var move_x = shell_win.event.move_x();
				
				if(move_x > 0){
					$($(com).children()[2]).hide();
				}else if(move_x < 0){
					if(shell_win.patient.last_patient_hospital_row){
						$($(shell_win.patient.last_patient_hospital_row).children()[2]).hide();
					}
					$($(com).children()[2]).show();
					shell_win.patient.last_patient_hospital_row = com;
				}
			},
			/**删除医院信息内容*/
			del_hospital_info:function(com){
				$(com).parent().parent().remove();
//				$(com).parent().remove();
			},
			/**提交数据*/
			submit: function() {
				var is_touch = shell_win.event.is_touch();
				if(!is_touch) return;
				
				var info = shell_win.patient.get_data();
				
				if(!info) return;
				
				var url = "";
				if (info.Id) {
					url = "ST_UDTO_UpdateBSearchAccountVO";
				} else {
					url = "ST_UDTO_AddBSearchAccountVO";
				}
				
				ShellComponent.mask.save();
				//提交数据
				Shell.util.Server.ajax({
					type:"post",
					data:Shell.util.JSON.encode({entity:info}),
					url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/" + url
				}, function(data) {
					ShellComponent.mask.hide();
					if (data.success) {
						shell_win.patient.change_member_list(info,data.value);
						shell_win.page.back("#" + shell_win.page.lev.L2.now, "#" + shell_win.page.lev.L2.back);
						shell_win.patient.to_page();
					} else {
						shell_win.system.msg.error("content_config_patient_info_msg", data.msg);
					}
				});
			},
			/**更新缓存的就诊人信息*/
			change_member_list:function(info,id){
				if(!info.Id){
					info.Id = id;
					shell_win.memory.member.list.push(info);
				}else{
					var list = shell_win.memory.member.list,
						len = list.length;
					
					for(var i=0;i<len;i++){
						if(list[i].Id == info.Id){
							list[i] = info;
							break;
						}
					}
				}
			},
			/**获取需要保存的数据*/
			get_data:function(){
				var id = $("#patient_info_id").val(),
					name = $("#patient_info_name").val(),
					mobilecode = $("#patient_info_mobilecode").val();

				if (!name) {
					ShellComponent.messagebox.msg("姓名不能为空");
					return;
				}
				if (!mobilecode) {
					ShellComponent.messagebox.msg("手机号不能为空");
					return;
				}
				if(mobilecode && !Shell.util.Isvalid.isCellPhoneNo(mobilecode)){
					ShellComponent.messagebox.msg("手机号格式错误");
					return;
				}
				
				var list = $("#patient_info_hospital_list").children(),
					len = list.length,
					SearchList = [];
					
				//是否已经存在在数值	
				var has_value = function(value){
					var length = SearchList.length,
						bo = false;
					for(var i=0;i<length;i++){
						if(SearchList[i].FieldsValue == value){
							bo = true;break;
						}
					}
					return bo;
				};
					
				for(var i=0;i<len;i++){
					var info = $(list[i]).children(),
						FieldsValue = $(info[0].firstChild).val() || "",
						Comment = $(info[1].firstChild).val() || "";
						
					if(!FieldsValue){
						ShellComponent.messagebox.msg("就诊卡/病历号不能为空，如不需要请删除");
						return;
					}
					if(has_value(FieldsValue)){
						ShellComponent.messagebox.msg("存在相同的就诊卡/病历号，请删除后再保存");
						return;
					}
					SearchList.push({
						FieldsCode:"PatNo",
						FieldsValue:FieldsValue,
						Comment:Comment,
						DispOrder:(i + 1) + ""
					});
					SearchList.push({
						FieldsCode:"VisNo",
						FieldsValue:FieldsValue,
						Comment:Comment,
						DispOrder:(i + 1) + ""
					});
				}
				
				var data = {
					Name:name,
					MobileCode:mobilecode,
					SearchList:SearchList
				};
				data.SearchList.push({
					FieldsCode:"MobileCode",
					FieldsValue:mobilecode
				});
				if(id){
					data.Id = id;
				}
				
				return data;
			},
			/**删除数据*/
			del: function(id) {
				var event = arguments.callee.caller.arguments[0] || window.event;
				event.preventDefault(); //阻止默认动作即该链接不会跳转。
				event.stopPropagation(); //阻止冒泡事件，上级的单击事件不会被调用 
				
				ShellComponent.mask.del();
				//提交数据
				Shell.util.Server.ajax({
					url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/ST_UDTO_DelBSearchAccount?id=" + id
				}, function(data) {
					ShellComponent.mask.hide();
					if (data.success) {
						var list = shell_win.memory.member.list,
							len = list.length;
						for (var i = 0; i < len; i++) {
							if (list[i].Id == id) {
								list.splice(i, 1);
								break;
							}
						}
						//shell_win.patient.to_page();
						$("#" + shell_win.patient.grid_row_id + id).remove();
					} else {
						ShellComponent.messagebox.msg(data.msg);
					}
				});

				return false;
			},
			/**获取就诊信息*/
			get_info_from_server:function(id,callback){
				shell_win.memory.member.get_list(function(list){
					var len = list.length,
						data = null;
					for(var i=0;i<len;i++){
						if(list[i].Id == id){
							data = list[i];
						}
					}
					callback(data);
				});
			}
		},
		/**账户*/
		account:{
			/**显示页面*/
			to_page:function(){
				shell_win.page.show(shell_win.account.get_html,"账号维护",1);
			},
			/**获取页面*/
			get_html:function(callback){
				shell_win.memory.account.get_info(function(info){
					var html = [];
					
					html.push('<div class="report_grid_view">');
					
					html.push('<div class="report_grid_row"');
					html.push(' on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.account.on_row_touch(\'password\')">');
					html.push('<div class="report_grid_row_name">修改密码</div>');
					html.push('<div class="report_grid_row_right">〉</div>');
					html.push('</div>');
					
					html.push('<div class="report_grid_row">');
					html.push('<div class="report_grid_row_name">开启登录</div>');
					html.push('<div class="report_grid_row_right">');
					html.push('<input id="account_login_status" class="ace ace-switch ace-switch-6" type="checkbox"');
					if(info.LoginInputPasswordFlag){
						html.push(' checked="checked"');
					}
					html.push(' onclick="shell_win_all.account.on_change_login_status()">');
					html.push('<span class="lbl"></span>');
					html.push('</div>');
					html.push('</div>');
					
//					html.push('<div class="report_grid_row"');
//					html.push(' on');
//					html.push(Shell.util.Event.touch);
//					html.push('="shell_win_all.account.clear_cache()">');
//					html.push('<div class="report_grid_row_name">清理缓存</div>');
//					html.push('</div>');
					
					html.push('</div>');
					
					callback(html.join(""));
				});
			},												
			/**数据行触摸处理*/
			on_row_touch:function(id){
				shell_win.account[id].to_page();
			},
			/**更改开启登录状态*/
			on_change_login_status:function(){
				Shell.util.Event.delay(function(){
					var checked = $("#account_login_status")[0].checked;
					ShellComponent.mask.save();
					Shell.util.Server.ajax({
						url:Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/ChangeLoginPasswordFlag?Flag=" + checked
					},function(data){
						ShellComponent.mask.hide();
						if(data.success){
							shell_win.memory.account.info.LoginInputPasswordFlag = checked;
						}else{
							ShellComponent.messagebox.msg(data.msg);
						}
					});
				});
			},
			/**密码*/
			password:{
				/**显示页面*/
				to_page:function(){
					var html = shell_win.account.password.get_html();
					shell_win.page.show(html, "密码修改",2);
				},
				get_html:function(){
					var html = [];
					
					html.push(ShellComponent.create_input({
						id: "account_password_info_pwd_old",
						placeholder: "当前密码"
					}));
					html.push(ShellComponent.create_input({
						id: "account_password_info_pwd_new",
						placeholder: "新密码"
					}));
					html.push(ShellComponent.create_input({
						id: "account_password_info_pwd_new2",
						placeholder: "确认新密码"
					}));
					
					html.push('<div class="shell_form_button_div" style="margin:50px 20%;">');
					html.push('<button on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.account.password.submit();">保存</button>');
					html.push('</div>');
					
					return html.join("");
				},
				/**保存数据*/
				submit:function(){
					var pwd_old = $("#account_password_info_pwd_old").val(),
						pwd_new = $("#account_password_info_pwd_new").val(),
						pwd_new2 = $("#account_password_info_pwd_new2").val();
						
					if(!pwd_old || !pwd_new || !pwd_new2){
						ShellComponent.messagebox.msg("密码不能为空");
						return;
					}
					if(pwd_new != pwd_new2){
						ShellComponent.messagebox.msg("请确保新密码两次输入相同");
						return;
					}
					
					Shell.util.Server.ajax({
						url:Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/Capchcwoaduntnge?" + 
						"OldPwd=" + pwd_old + "&NewPwd=" + pwd_new
					},function(data){
						if(data.success){
							ShellComponent.messagebox.msg("密码修改成功");
							shell_win.page.back("#"+shell_win.page.lev["L2"].now,"#"+shell_win.page.lev["L2"].back);
						}else{
							data.msg = data.msg ? data.msg : "密码错误！";
							ShellComponent.messagebox.msg(data.msg);
						}
					});
				}
			},
			/**清理缓存*/
			clear_cache:function(){
				ShellComponent.mask.to_server("缓存清理中...");
				$.ajax({
					url:Shell.util.Path.uiPath + '/index.html',
					dataType:'html',
					data:{},
					cache:false,
					ifModified :true ,
					success:function(response){
						ShellComponent.mask.hide();
					}
				});
			}
		},
		/**页面*/
		page:{
			/**屏幕高度*/
			page_width:0,
			/**屏幕宽度*/
			page_height:0,
			lev: {
				"L1": {
					now: "page_info_1",
					back: "page_home"
				},
				"L2": {
					now: "page_info_2",
					back: "page_info_1"
				},
				"L3": {
					now: "page_info_3",
					back: "page_info_2"
				}
			},
			/**回退页面*/
			back: function(page_now, back_to) {
				$(page_now).hide();
				$(back_to).show();
			},
			/**显示页面*/
			show: function(html, title, num,after) {
				var L = "L" + (num ? num : "1");
				$("#" + shell_win.page.lev[L].back).hide();
				shell_win.page.show_content(html, title, L,function(data){
					$("#" + shell_win.page.lev[L].now).html(data).show();
					if(after) after();
				});
			},
			/**显示内容*/
			show_content:function(html, title, L,callback){
				if(typeof(html) === "function"){
					html(function(data){
						callback(shell_win.page.get_content(data,title,L));
					});
				}else if(typeof(html) === "string"){
					callback(shell_win.page.get_content(html,title,L));
				}
			},
			/**获取内容*/
			get_content:function(html,title,L){
				var div = [];
				
				div.push('<div class="page_head_fixed">');
				
//				if(L != "L1"){
					div.push('<div class="page_info_head_back" on' + Shell.util.Event.touch +
					'="shell_win_all.page.back(\'#' + shell_win.page.lev[L].now +
					'\',\'#' + shell_win.page.lev[L].back + '\');">〈 返回 </div>');
//				}

				if (title) {
					div.push('<div class="page_info_head_title">');
					div.push(title);
					div.push('</div>');
				}

				div.push('</div>');
				div.push('<div class="page_content" style="margin-bottom: 0;">');
				div.push(html);
				div.push('</div>');
				
				return div.join("");
			},
			/**初始化*/
			init:function(){
				shell_win.page.page_width = document.body.scrollWidth;
				shell_win.page.page_height = document.body.scrollHeight;
			}
		},
		/**动作*/
		event: {
			/**开始触摸点X位置*/
			touch_start_x: null,
			/**开始触摸点Y位置*/
			touch_start_y: null,
			/**结束触摸点X位置*/
			touch_end_x: null,
			/**结束触摸点Y位置*/
			touch_end_y: null,
			/**X轴移动距离*/
			move_x:function(){
				if(shell_win.event.touch_end_x == null || 
					shell_win.event.touch_start_x == null) return null;
				return shell_win.event.touch_end_x - shell_win.event.touch_start_x;
			},
			/**Y轴移动距离*/
			move_y:function(){
				if(shell_win.event.touch_end_y == null || 
					shell_win.event.touch_start_y == null) return null;
				return shell_win.event.touch_end_y - shell_win.event.touch_start_y;
			},
			/**初始化*/
			init:function(){
				var page = window.document.body;
				page.addEventListener("touchstart", function(e) {
					if (e.targetTouches.length == 1 && !self.busy) {
						var xy = e.targetTouches[0];
						shell_win.event.touch_start_x = xy.pageX;
						shell_win.event.touch_start_y = xy.pageY;
						shell_win.event.touch_end_x = xy.pageX;
						shell_win.event.touch_end_y = xy.pageY;
					}
				}, false);
				page.addEventListener("touchmove", function(e) {
					if (e.targetTouches.length == 1 && !self.busy) {
						var xy = e.targetTouches[0];
						shell_win.event.touch_end_x = xy.pageX;
						shell_win.event.touch_end_y = xy.pageY;
					}
				}, false);
				page.addEventListener("touchend", function(e) {
					
				}, false);
			},
			/**是否是触摸*/
			is_touch:function(){
				var move_x = this.move_x(),
					move_y = this.move_y(),
					bo = true;
					
				if(move_x && (move_x > 10 || move_x < -10))
					bo = false;
					
				if(move_y && (move_y > 10 || move_y < -10))
					bo = false;
					
				return bo;
			}
		},
		/**微信功能*/
		weixin: {
			signature: null,
			timeout: null,
			timestamp: 1414587457,
			url: window.document.location.href.split("#")[0],
			nonceStr: "AAAA",
			error_no_signature: "没有获取到微信签名,请刷新页面",
			/**微信功能初始化*/
			init: function() {
				//获取微信签名
				shell_win.weixin.get_signature(shell_win.weixin.set_sonfig);
			},
			/**设置微信参数*/
			set_sonfig: function() {
				wx.config({
					debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
					appId: 'wx359def2eeed3abe6', // 必填，公众号的唯一标识
					timestamp: shell_win.weixin.timestamp, // 必填，生成签名的时间戳
					nonceStr: shell_win.weixin.nonceStr, // 必填，生成签名的随机串
					signature: shell_win.weixin.signature, // 必填，签名，见附录1
					jsApiList: ['scanQRCode'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
				});
				if (shell_win.weixin.timeout && shell_win.weixin.timeout > 0) {
					setTimeout(shell_win.weixin.get_signature, shell_win.weixin.timeout * 1000);
				}
			},
			/**获取微信签名*/
			get_signature: function(callback) {
				var call = Shell.util.typeOf(callback) === "function" ? [callback] : 
					Shell.util.typeOf(callback) === "array" ? callback : [];
				ShellComponent.mask.to_server("微信签名获取中...");
				Shell.util.Server.ajax({
					url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/GetJSAPISignature?" +
						"noncestr=" + shell_win.weixin.nonceStr + "&timestamp=" + shell_win.weixin.timestamp +
						"&url=" + shell_win.weixin.url
				}, function(data) {
					ShellComponent.mask.hide();
					if (data.success) {
						shell_win.weixin.signature = data.value.signature;
						shell_win.weixin.timeout = data.value.TimeOut;
						for(var i in call){
							call[i]();
						}
					} else {
						ShellComponent.messagebox.msg(shell_win.weixin.error_no_signature);
					}
				});
			},
			/**扫条码*/
			scan:function(callback){
				if (shell_win.weixin.signature) {
					wx.scanQRCode({
						needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
						scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
						success: function(res) {
							// 当needResult 为 1 时，扫码返回的结果
							callback(res.resultStr);
						}
					});
				} else {
					shell_win.weixin.get_signature([shell_win.weixin.set_sonfig,function(){
						setTimeout(function(){
							wx.scanQRCode({
								needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
								scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
								success: function(res) {
									// 当needResult 为 1 时，扫码返回的结果
									callback(res.resultStr);
								}
							});
						},300);
					}]);
				}
			}
		},
		/**初始化*/
		init:function(){
			//系统初始化
			shell_win.system.init();
			
			if(!shell_win.system.open_id){
				window.location.href = "login.html";
				return;
			}
			//页面初始化
			shell_win.page.init();
			//首页初始化
			shell_win.home.init();
			//初始化页面动作
			shell_win.event.init();
			//微信功能初始化
			//shell_win.weixin.init();
			//定位功能
			var arr = window.document.location.href.split("#");
			if(arr.length >= 2 && arr[1]!=''){
				shell_win.home.on_icon_touch(arr[1]);
			}
		}
	};
	//公开全局对象
	shell_win_all = shell_win;
	//初始化页面
	shell_win_all.init();
}); 