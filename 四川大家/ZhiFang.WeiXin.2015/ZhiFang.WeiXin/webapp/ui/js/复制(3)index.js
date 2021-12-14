var shell_win_all = null;
$(function() {
	//页面所有功能对象
	var shell_win = {
		/**系统*/
		system: {

			/**微信ID*/
			open_id: null,
			/**用户ID*/
			user_id: null,
			/**系统初始化*/
			init: function() {
				shell_win.system.open_id = Shell.util.Cookie.getCookie("openId");
				shell_win.system.user_id = Shell.util.Cookie.getCookie("userId");
			}
		},
		/**缓存*/
		memory:{
			/**成员*/
			member: {
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
					shell_win.util.mask.loading();
					Shell.util.Server.ajax({
						url: Shell.util.Path.rootPath + 
						"/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetBSearchAccountVOListByWeiXinAccountId"
					}, function(data) {
						shell_win.util.mask.hide();
						if (data.success) {
							shell_win.memory.member.list = data.value || [];
							shell_win.memory.member.has_load = true;
							callback();
						} else {
							shell_win.memory.member.list = [];
							callback();
							alert(data.msg);
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
			}
		},
		/**公用功能*/
		util:{
			/**数据映射*/
			map: {
				/**性别*/
				sex: {
					"1": "男",
					"2": "女"
				}
			},
			/**蒙版*/
			mask: {
				/**隐藏信息*/
				hide: function() {
					$("#page_mask_loading_div").hide();
					$("#page_mask_info_div").hide();
					$("#page_mask_div").removeClass("page_mask");
				},
				/**加载数据*/
				loading:function(){
					shell_win.util.mask.to_server("数据加载中...");
				},
				/**保存数据*/
				save:function(){
					shell_win.util.mask.to_server("数据保存中...");
				},
				/**删除数据*/
				del:function(){
					shell_win.util.mask.to_server("数据删除中...");
				},
				/**交互数据*/
				to_server:function(msg){
					$("#page_mask_div").addClass("page_mask");
					$("#page_mask_loading_div_text").html(msg || "");
					$("#page_mask_loading_div").show();
				},
				/**交互信息*/
				msg:function(config){
					var con = config || {},
						html = [];
					//标题
					if(con.title){
						html.push('<span>');
						html.push(con.title);
						html.push('</span>');
					}
					//消息
					if(con.msg){
						html.push('<span>');
						html.push(con.msg);
						html.push('</span>');
					}
					//输入框
					if(con.input){
						html.push('<input placeholder="');
						if(con.input_placeholder){
							html.push(con.input_placeholder);
						}
						html.push('"></input>');
					}
					
					html.push('<div>');
					if(con.ok){
						
					}
					html.push('</div>');
					if(con.cancel){
						
					}
				}
			}
		},
		/**组件*/
		unit: {
			/**获取带图标输入框*/
			get_input: function(config) {
				config = config || {};
				var input =
					'<div class="input-group" style="margin-bottom:10px;">' +
					'<span class="input-group-addon"><i class="' + (config.iconCls || '') + '"></i>' + (config.label || '') + '</span>' +
					'<input class="form-control" type="text" id="' + config.id + '" placeholder="' +
					config.placeholder + '" value="' + (config.value || '') + '">' +
					'</div>';
				return input;
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
					num = 3;
				
				html.push('<table class="home_icon_table">');
				for(var i=0;i<len;i++){
					if(i % num == 0){
						if(i == 0){
							html.push('<tr>');
						}else{
							html.push('</tr><tr>');
						}
					}
					html.push('<td>');
					html.push(shell_win.home.create_icon(list[i]));
					html.push('</td>');
				}
				html.push('</table>');
				
				$("#page_home").html(html.join(""));
			},
			/**获取功能列表*/
			get_icon_list:function(){
				var list = [{
					id:"barcode",
					name:"扫一扫",
					icon:Shell.util.Path.uiPath + "/img/icon/barcode.png"
				},{
					id:"report",
					name:"近期报告",
					icon:Shell.util.Path.uiPath + "/img/icon/report.png"
				},{
					id:"patient",
					name:"就诊人",
					icon:Shell.util.Path.uiPath + "/img/icon/patient.png"
				},{
					id:"account",
					name:"账号",
					icon:Shell.util.Path.uiPath + "/img/icon/account.png"
				},{
					id:"report2",
					name:"历史报告",
					icon:Shell.util.Path.uiPath + "/img/icon/report.png"
				}];
				return list;
			},
			/**创建图标*/
			create_icon:function(info){
				
				if(!info || !info.id || !info.name) return null;
				
				var icon_id = shell_win.home.icon_id + info.id,
					html = [];
				
				html.push('<div id="' + icon_id + '" class="home_icon_div" on' + Shell.util.Event.touch + 
					'="shell_win_all.home.on_icon_touch(\'' + info.id + '\');">');
				html.push('<img src="' + info.icon + '"/>');
				html.push('<span>' + info.name + '</span>');
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
			/**显示页面*/
			to_page:function(){
				shell_win.page.show(shell_win.barcode.get_html,"扫一扫",1);
			},
			/**获取页面*/
			get_html:function(callback){
				shell_win.memory.member.get_list(function(data){
					var len = data.length,
						html = [];
					
					html.push('<ul class="list-group">');
					for(var i=0;i<len;i++){
						html.push('<li class="list-group-item" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.barcode.on_member_row_touch(\'');
						html.push(data[i].Id);
						html.push('\',\'');
						html.push(data[i].Name);
						html.push('\')">');
						html.push('<span class="list_group_item_right_arrow">〉</span>');
						html.push(data[i].Name);
						html.push('</li>');
					}
					html.push('</ul>');
					
					callback(html.join(""));
				});
			},
			/**数据行触摸处理*/
			on_member_row_touch:function(id,name){
				shell_win.weixin.scan(function(value){
					var barcode = value.split(",").slice(-1);
					shell_win.barcode.to_report(barcode,name);
				});
			},
			/**查询报告*/
			to_report: function(bacode, name) {
				shell_win.util.mask.loading();
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
					shell_win.util.mask.hide();
					shell_win.page.show(html.join(""), "报告信息",2);
				})
			}
		},
		/**报告查询-近期*/
		report:{
			/**最后一次触摸的成员数据行*/
			last_member_id:null,
			/**查询天数*/
			days:30,
			/**缓存列表数据*/
			list:[],
			/**对象属性映射*/
			obj: {
				/**属性字段*/
				info: "info",
				/**列表字段*/
				list: "list",
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
					report_time: "ReportTime"
				},
				/**列表内容*/
				list_obj: {
					/**项目*/
					items: "ItemsName",
					/**结果*/
					result: "Result",
					/**单位*/
					unit: "Unit",
					/**参考值*/
					reference_value: "ReferenceValue"
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
						html = [];
					
					html.push('<div style="margin-bottom:5px;font-size:14px;margin-left:15px;color:gray;">就诊人</div>');
					html.push('<ul id="report_member_ul" class="list-group">');
					
					for(var i=0;i<len;i++){
						html.push('<li class="list-group-item" id="report_member_li_');
						html.push(data[i].Id);
						html.push('" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.report.on_member_row_touch(\'');
						html.push(data[i].Id);
						html.push('\',\'');
						html.push(data[i].Name);
						html.push('\',\'');
						html.push(data[i].RFIndexList || '');
						html.push('\')">');
						html.push('<span class="list_group_item_right_arrow">〉</span>');
						if(data[i].UnReadRFCount > 0){
							html.push('<span class="badge" style="background-color:#3c763d" id="report_member_li_');
							html.push(data[i].Id);
							html.push('_badge">');
							html.push(data[i].UnReadRFCount);
							html.push('</span>');
						}
						html.push(data[i].Name);
						html.push('</li>');
					}
					html.push('</ul>');
					html.push('<div style="margin-bottom:5px;font-size:14px;margin-left:15px;color:gray;">报告列表</div>');
					html.push('<ul id="report_ul_view" class="list-group"></ul>');
					
					callback(html.join(""));
				});
			},
			/**数据行触摸处理*/
			on_member_row_touch:function(id,name,report_ids){
				if(shell_win.report.last_member_id){
					$("#report_member_li_" + shell_win.report.last_member_id).removeClass("button_touch");
				}
				shell_win.report.last_member_id = id;
				$("#report_member_li_" + id).addClass("button_touch");
				
				if(!report_ids) return;
				
				shell_win.report.get_report_list(id,name,report_ids,function(data){
					var len = data.length,
						html = [];
					
					for(var i=0;i<len;i++){
						html.push('<li class="list-group-item" id="report_report_li_');
						html.push(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]);
						html.push('" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.report.on_report_row_touch(\'');
						html.push(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]);
						html.push('\')">');
						html.push(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.patient_name]);
						html.push('<span style="margin-left:20px;">');
						html.push(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.visit_time]);
						html.push('<span style="margin-left:20px;">');
						html.push(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.hospital_name]);
						html.push('</span>');
						
						var has_read = shell_win.report.has_read(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]);
						if(!has_read){
							html.push(
								'<div id="report_member_li_has_read_mark_' + data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id] + '" style="float:right;margin-top:-5px">' +
									'<button class="btn btn-xs btn-success" style="padding:5px;"' + 
										'<i>未读</i>' +
									'</button>' + 
								'</div>'
							);
						}
						
						html.push('</li>');
					}
						
					if(len == 0){
						html.push('<li class="list-group-item">没有报告</li>');
					}
						
					$("#report_ul_view").html(html.join(""));
				});
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
			get_report_list:function(id,name,report_ids,callback){
				shell_win.util.mask.loading();
				Shell.util.Server.ajax({
					type:"post",
					//data:Shell.util.JSON.encode({ReportFormIndexIdList:report_ids}),
					data:"{\"ReportFormIndexIdList\":\"" + report_ids + "\"}",
					url:Shell.util.Path.rootPath + 
					"/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetSearchAccountReportFormListById"
//					"/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetBSearchAccountRFList" + 
//					"?SearchAccountId=" +id + "&Name=" + name
				},function(data){
					shell_win.util.mask.hide();
					var html = "";
					shell_win.report.list = [];
					if(data.success){
						shell_win.report.list = data.value;
						var len = shell_win.report.list.length;
						for(var i=0;i<len;i++){
							shell_win.report.list[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.patient_id] = id;
						}
						callback(shell_win.report.list);
					}else{
						callback(shell_win.report.list);
						alert(data.msg);
					}
				});
			},
			/**报告列表行触摸处理*/
			on_report_row_touch:function(id){
				var list = shell_win.report.list,
					len = list.length,
					html = [];
					
				for(var i=0;i<len;i++){
					if(list[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id] == id){
						html.push(shell_win.report.get_report_info_html(list[i]));
						shell_win.report.report_load(list[i]);
						break;
					}
				}
				shell_win.page.show(html.join(""), "报告信息",2);
			},
			/**报告已读处理*/
			report_load:function(info){
				var patient_id = info[shell_win.report.obj.info][shell_win.report.obj.info_obj.patient_id];
				
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
						
						$("#report_report_li_" + info[shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]).remove();
						shell_win.report.report_load_to_server(
							patient_id,
							info[shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]
						);
						break;
					}
				}
			},
			/**报告已读标记*/
			report_load_to_server:function(member_id,report_id){
				Shell.util.Server.ajax({
					url:Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetSearchAccountReportFormById?" + 
						"ReportFormIndexId=" + report_id + "&SearchAccountId=" + member_id
				},function(data){
					shell_win.memory.member.remove(member_id,report_id);
					$("#report_member_li_has_read_mark_" + report_id).remove();
				});
			},
			/**获取报告信息页面内容*/
			get_report_info_html: function(data) {
				var obj = shell_win.report.obj;
					value = data || {},
					info = value[obj.info] || {},
					div = [];

				//出单医院
				div.push('<div class="report_title_div">');
				div.push(info[obj.info_obj.hospital_name]);
				div.push('</div>');

				div.push('<div class="report_info_div">');
				//原始报告按钮
				//div.push('<button class="report_info_div_button">原始报告</button>');
				//检验单号
				div.push('<span>检验单号：');
				div.push(info[obj.info_obj.check_list_number]);
				div.push('</span></br>');
				//患者姓名
				div.push('<span>患者姓名：');
				div.push(info[obj.info_obj.patient_name]);
				div.push('</span></br>');
				//就诊时间
				div.push('<span>就诊时间：');
				div.push(info[obj.info_obj.visit_time]);
				div.push('</span></br>');
				//报告时间
				div.push('<span>报告时间：');
				div.push(info[obj.info_obj.report_time]);
				div.push('</span>');
				div.push('</div>');

				//项目内容列表
				var list = value[obj.list],
					len = list.length,
					list_table = [];

				list_table.push('<table class="report_items_table">');
				list_table.push('<thead><th>检验项目</th><th>结果</th><th>单位</th><th>参考值</th></thead>');
				list_table.push('<tbody>');
				for (var i = 0; i < len; i++) {
					var row = list[i];
					list_table.push('<tr>');
					list_table.push('<td>');
					list_table.push(row[obj.list_obj.items]);
					list_table.push('</td>');
					list_table.push('<td>');
					list_table.push(row[obj.list_obj.result]);
					list_table.push('</td>');
					list_table.push('<td>');
					list_table.push(row[obj.list_obj.unit]);
					list_table.push('</td>');
					list_table.push('<td>');
					list_table.push(row[obj.list_obj.reference_value]);
					list_table.push('</td>');
					list_table.push('</tr>');
				}
				list_table.push('</tbody>');
				list_table.push('</table>');

				div.push(list_table.join(""));

				return div.join("");
			}
		},
		/**历史报告*/
		report2:{
			/**最后一次触摸的成员头像*/
			last_member_id:null,
			/**最后一次触摸的报告列表行*/
			last_report_id:null,
			/**当前页签*/
			page:1,
			/**每页数量*/
			limit:10,
			/**缓存列表数据*/
			list:[],
			
			/**显示页面*/
			to_page:function(){
				shell_win.page.show(shell_win.report2.get_html,"历史报告",1);
			},
			/**获取页面*/
			get_html:function(callback){
				shell_win.memory.member.get_list(function(data){
					var len = data.length,
						photos = [],
						html = [];
					
					for(var i=0;i<len;i++){
						photos.push('<div class="user_photo" id="report2_member_photo_');
						photos.push(data[i].Id);
						photos.push('" on');
						photos.push(Shell.util.Event.touch);
						photos.push('="shell_win_all.report2.on_member_photo_touch(\'');
						photos.push(data[i].Id);
						photos.push('\',\'');
						photos.push(data[i].Name);
						photos.push('\')">');
						
						photos.push('<img src="');
						photos.push(Shell.util.Path.uiPath + "/img/win8/appbar.user.png");
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
		
						html.push('<div id="report2_grid_view"></div>');
		
						callback(html.join(""));
					}else{
						callback("没有就诊人信息，清先进行维护");
					}
				});
			},
			/**头像触摸处理*/
			on_member_photo_touch:function(id,name){
				if(shell_win.report2.last_member_id){
					$("#report2_member_photo_" + shell_win.report2.last_member_id).removeClass("button_touch");
				}
				shell_win.report2.last_member_id = id;
				$("#report2_member_photo_" + id).addClass("button_touch");
				
				shell_win.report2.get_report_list(id,name,function(data){
					var len = data.length,
						html = [];
					
					for(var i=0;i<len;i++){
						html.push('<div class="report_grid_row" id="report2_report_row_');
						html.push(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]);
						html.push('" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.report2.on_report_row_touch(\'');
						html.push(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]);
						html.push('\')">');
						html.push('<div class="report_grid_row_date">');
						html.push(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.visit_time]);
						html.push('</div>');
						html.push('<div class="report_grid_row_name">');
						html.push(data[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.hospital_name]);
						html.push('</div>');
						
						html.push('</div>');
					}
						
					if(len == 0){
						html.push('<div style="text-align:center;margin:20px;">没有报告</div>');
					}
						
					$("#report2_grid_view").html(html.join(""));
				});
			},
			/**是否已阅读*/
			has_read:function(id){
				var data = shell_win.memory.member.list,
					len = data.length,
					info = {};
				for(var i=0;i<len;i++){
					if(data[i].Id == shell_win.report2.last_member_id){
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
				shell_win.util.mask.loading();
				Shell.util.Server.ajax({
					url:Shell.util.Path.rootPath + 
					"/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetBSearchAccountRFList" + 
					"?SearchAccountId=" +id + "&Name=" + name
				},function(data){
					shell_win.util.mask.hide();
					var html = "";
					shell_win.report2.list = [];
					if(data.success){
						shell_win.report2.list = data.value;
						var len = shell_win.report2.list.length;
						for(var i=0;i<len;i++){
							shell_win.report2.list[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.patient_id] = id;
						}
						callback(shell_win.report2.list);
					}else{
						callback(shell_win.report2.list);
						alert(data.msg);
					}
				});
			},
			/**报告列表行触摸处理*/
			on_report_row_touch:function(id){
				if(shell_win.report2.last_member_id){
					$("#report2_report_row_" + shell_win.report2.last_report_id).removeClass("button_touch");
				}
				shell_win.report2.last_report_id = id;
				$("#report2_report_row_" + id).addClass("button_touch");
				
				var list = shell_win.report2.list,
					len = list.length,
					html = [];
					
				for(var i=0;i<len;i++){
					if(list[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id] == id){
						html.push(shell_win.report.get_report_info_html(list[i]));
						var has_read = shell_win.report2.has_read(list[i][shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]);
						if(!has_read){
							shell_win.report.report_load(list[i]);
						}
						break;
					}
				}
				shell_win.page.show(html.join(""), "报告信息",2);
			},
			/**报告已读处理*/
			report_load:function(info){
				var patient_id = info[shell_win.report.obj.info][shell_win.report.obj.info_obj.patient_id];
				
				var list = shell_win.memory.member.list,
					len = list.length;
					
				for(var i=0;i<len;i++){
					if(list[i].Id == patient_id){
						if(list[i].UnReadRFCount > 0) list[i].UnReadRFCount--;
						if(list[i].UnReadRFCount == 0){
							$("#report2_member_li_" + patient_id + "_badge").remove();
						}else{
							$("#report2_member_li_" + patient_id + "_badge").html(list[i].UnReadRFCount);
						}
						$("#report2_li_status_" + info[shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]).remove();
						shell_win.report.report_load_to_server(
							info[shell_win.report.obj.info][shell_win.report.obj.info_obj.patient_id],
							info[shell_win.report.obj.info][shell_win.report.obj.info_obj.report_id]
						);
						break;
					}
				}
			}
		},
		/**就诊人*/
		patient:{
			/**最后一次触摸的就诊人列表行*/
			last_patient_id:null,
			/**显示页面*/
			to_page:function(){
				shell_win.page.show(shell_win.patient.get_html,"就诊人维护",1);
			},
			/**获取页面*/
			get_html:function(callback){
				shell_win.memory.member.get_list(function(data){
					var len = data.length,
						html = [];
					
//					html.push('<ul class="list-group">');
//					html.push('<li class="list-group-item"><button class="btn btn-xs btn-success" style="float: right;" on');
//					html.push(Shell.util.Event.touch);
//					html.push('="shell_win_all.patient.on_row_touch();"><i class="glyphicon glyphicon-plus"></i></button>&nbsp;</li>');
//					for(var i=0;i<len;i++){
//						html.push('<li class="list-group-item" on');
//						html.push(Shell.util.Event.touch);
//						html.push('="shell_win_all.patient.on_row_touch(\'');
//						html.push(data[i].Id);
//						html.push('\')">');
//						html.push('<span class="list_group_item_right_arrow">〉</span>');
//						html.push(
//							'<div style="float:right;margin-top:-5px" hidden="hidden" id="patient_li_' + 
//								data[i].Id + '">' +
//								'<button class="btn btn-xs btn-danger" style="padding:5px 15px;" on' + 
//								Shell.util.Event.touch +
//								'="shell_win_all.patient.del(\'' + data[i].Id + '\');">' + 
//									'<i class="glyphicon glyphicon-trash"></i>' + 
//								'</button>' + 
//							'</div>'
//						);
//						html.push(data[i].Name);
//						html.push('</li>');
//					}
//					html.push('</ul>');
					
					html.push('<div>');
					for(var i=0;i<len;i++){
						html.push('<div class="patient_grid_row" id="patient_row_');
						html.push(data[i].Id);
						html.push('" on');
						html.push(Shell.util.Event.touch);
						html.push('="shell_win_all.patient.on_row_touch(\'');
						html.push(data[i].Id);
						html.push('\')">');
						html.push('<img src="');
						html.push(data[i].Icon || (Shell.util.Path.uiPath + "/img/win8/appbar.user.png"));
						html.push('"/>');
						html.push('<span>');
						html.push(data[i].Name);
						html.push('</span>');
						html.push('</div>');
					}
					
					html.push('<div class="patient_grid_row" id="patient_row_0" on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.patient.on_row_touch(\'0\')">');
					html.push('<img src="');
					html.push(Shell.util.Path.uiPath);
					html.push('/img/win8/appbar.add.png"/>');
					html.push('<span>新增就诊人</span>');
					html.push('</div>');
					
					callback(html.join(""));
				});
			},
			/**数据行触摸处理*/
			on_row_touch:function(id){
				if(shell_win.patient.last_patient_id){
					$("#patient_row_" + shell_win.patient.last_patient_id).removeClass("button_touch");
				}
				shell_win.patient.last_patient_id = id;
				$("#patient_row_" + id).addClass("button_touch");
				
				if (!id || id == "0") { //新增
					var html = shell_win.patient.get_info_page_add();
					shell_win.page.show(html, "就诊人信息维护", 2);
				} else { //修改
					var move_x = shell_win.event.move_x();
					if(move_x == null || move_x == 0){
						shell_win.patient.get_info_page_edit(id,function(html){
							shell_win.page.show(html, "就诊人信息维护", 2);
						});
					}else if(move_x > 0){
						$("#patient_li_" + id).hide();
					}else if(move_x < 0){
						$("#patient_li_" + id).show();
					}
				}
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
					'<button id="patient_info_submit_btn" class="btn btn-primary" style="width: 100%;margin-top:15px;" on' +
					Shell.util.Event.touch + '="shell_win_all.patient.submit();">确定</button>' + 
				'</div>';

				return html;
			},
			/**获取就诊人信息内容*/
			get_info_member:function(info){
				var data = info || {};
				var html =
					'<input type="hidden" id="patient_info_id" value="' + (data.Id || '') + '"></input>' +
					shell_win.unit.get_input({
						id: "patient_info_name",
						iconCls: "glyphicon glyphicon-user",
						placeholder: "请输入姓名",
						value:data.Name || ""
					}) + 
					shell_win.unit.get_input({
						id: "patient_info_mobilecode",
						iconCls: "glyphicon glyphicon-phone",
						placeholder: "请输入手机号",
						value:data.MobileCode || ""
					});
//					shell_win.unit.get_input({
//						id: "patient_info_idnumber",
//						iconCls: "glyphicon glyphicon-user",
//						placeholder: "请输入身份证号",
//						label:"身份证",
//						value:data.IDNumber || ""
//					}) + 
//					shell_win.unit.get_input({
//						id: "patient_info_medicare",
//						iconCls: "glyphicon glyphicon-user",
//						placeholder: "请输入医保卡号",
//						label:"医保卡",
//						value:data.MediCare || ""
//					});
					
				return html;
			},
			/**获取就诊人医院信息内容*/
			get_info_hospital:function(info){
				var data = info || {},
					list = data.SearchList || [],
					len = list.length,
					html = [];
					
				html.push(
					'<div style="padding:5px;border-bottom: 1px solid #e7e7e7;">' +
						'<span style="color:#000000;font-weight:bold">就诊医院信息<span>' +
						'<button class="btn btn-xs btn-success" style="float: right;" on' + Shell.util.Event.touch + 
						'="shell_win_all.patient.add_hospital_info();"><i class="glyphicon glyphicon-plus"></i></button>' +
					'</div>' +
					'<div id="patient_info_hospital_list">'
				);
				
				for(var i=0;i<len;i++){
					var code = list[i].FieldsCode;
					if(code != "MobileCode" && code != "MediCare" && code != "IDNumber"){
						html.push(shell_win.patient.get_hospital_row(list[i]));
					}
				}
				
				html.push('</div>');
					
				return html.join("");
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
				
				var html = 
				'<div class="member_hospital_div" name="patient_info_hospital_list_row">' +
					'<div class="input-group input-group-sm" style="float:left;">' + 
						'<select style="width:85px;" class="form-control">' + 
							'<option value="PatNo"' + (data.FieldsCode == 'PatNo' ? ' selected="selected"' : '') + '>病历号</option>' + 
							'<option value="VisNo"' + (data.FieldsCode == 'VisNo' ? ' selected="selected"' : '') + '>就诊卡</option>' + 
						'</select>' + 
					'</div>'+ 
					'<div class="input-group input-group-sm" style="float:left;margin-left:4px;">' + 
						'<input style="width:100px;" class="form-control" placeholder="输入框" value="' + (data.FieldsValue || '') + '">' + 
					'</div>'+ 
					'<div class="input-group input-group-sm" style="float:left;margin-left:4px;">' + 
						'<input style="width:100px;" class="form-control" placeholder="备注医院名称" value="' + (data.Comment || '') + '">' +
					'</div>'+ 
					'<div style="float:left;margin-left:4px;">' + 
						'<button class="btn btn-xs btn-danger" style="padding:5px 8px;" on' + Shell.util.Event.touch + 
						'="shell_win_all.patient.del_hospital_info(this);">' + 
							'<i class="glyphicon glyphicon-trash"></i>' + 
						'</button>' + 
					'</div>' +
				'</div>';
				
				return html;
			},
			/**删除医院信息内容*/
			del_hospital_info:function(com){
				$(com).parent().parent().remove();
			},
			/**提交数据*/
			submit: function() {
				var info = shell_win.patient.get_data();
				
				if(!info) return;
				
				var url = "";
				if (info.Id) {
					url = "ST_UDTO_UpdateBSearchAccountVO";
				} else {
					url = "ST_UDTO_AddBSearchAccountVO";
				}
				
				shell_win.util.mask.save();
				//提交数据
				Shell.util.Server.ajax({
					type:"post",
					data:Shell.util.JSON.encode({entity:info}),
					url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/" + url
				}, function(data) {
					shell_win.util.mask.hide();
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
//					idnumber = $("#patient_info_idnumber").val(),
//					medicare = $("#patient_info_medicare").val();

				if (!name) {
					alert("姓名不能为空");
					return;
				}
				if (!mobilecode) {
					alert("手机号不能为空");
					return;
				}
				if(mobilecode && !Shell.util.Isvalid.isCellPhoneNo(mobilecode)){
					alert("手机号格式错误");
					return;
				}
//				if(idnumber && !Shell.util.Isvalid.isIdCardNo(idnumber)){
//					alert("身份证号格式错误");
//					return;
//				}
//				if (!medicare) {
//					alert("医保卡不能为空");
//					return;
//				}
				
				var list = $("#patient_info_hospital_list").children(),
					len = list.length,
					SearchList = [];
					
				for(var i=0;i<len;i++){
					var info = $(list[i]).children();
					if(!$(info[1].firstChild).val()){
						alert("就诊卡或病历号不能为空，如不需要请删除");
						return;
					}
					SearchList.push({
						FieldsCode:$(info[0].firstChild).val(),
						FieldsValue:$(info[1].firstChild).val(),
						Comment:$(info[2].firstChild).val(),
						DispOrder:(i + 1) + ""
					});
				}
				
				var data = {
					Name:name,
					MobileCode:mobilecode,
//					IDNumber:idnumber || "",
//					MediCare:medicare,
					SearchList:SearchList
				};
				data.SearchList.push({
					FieldsCode:"MobileCode",
					FieldsValue:mobilecode
				});
//				data.SearchList.push({
//					FieldsCode:"MediCare",
//					FieldsValue:medicare
//				});
//				if(idnumber){
//					data.SearchList.push({
//						FieldsCode:"IDNumber",
//						FieldsValue:idnumber
//					});
//				}
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
				
				shell_win.util.mask.del();
				//提交数据
				Shell.util.Server.ajax({
					url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/ST_UDTO_DelBSearchAccount?id=" + id
				}, function(data) {
					shell_win.util.mask.hide();
					if (data.success) {
						var list = shell_win.memory.member.list,
							len = list.length;
						for (var i = 0; i < len; i++) {
							if (list[i].Id == id) {
								list.splice(i, 1);
								break;
							}
						}
						shell_win.patient.to_page();
					} else {
						alert(data.msg);
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
				var html = [];
				html.push('<ul class="list-group">');
				//密码修改
				html.push(shell_win.account.get_li({
					id:"password",
					name:"修改密码",
					fun:"on_row_touch"
				}));
				//开启登录
				html.push(shell_win.account.get_li_bool({
					id:"login",
					name:"开启登录",
					fun:"on_change_login_status"
				}));
				
				html.push('</ul>');
				
				callback(html.join(""));
			},
			/**获取通用行*/
			get_li:function(data){
				var html = [];
				
				html.push('<li class="list-group-item" on');
				html.push(Shell.util.Event.touch);
				html.push('="shell_win_all.account.');
				html.push(data.fun);
				html.push('(\'');
				html.push(data.id);
				html.push('\')">');
				html.push('<span class="list_group_item_right_arrow">〉</span>');
				html.push(data.name);
				html.push('</li>');
				
				return html.join("");
			},
			/**获取开关行*/
			get_li_bool:function(data){
				var html = [];
				
				html.push('<li class="list-group-item">');
				html.push(
					'<label class="list_group_item_right_arrow" style="margin-top:-3px;">' +
						'<input id="account_login_status" class="ace ace-switch ace-switch-6" type="checkbox" on' 
				);		
				html.push(Shell.util.Event.touch);
				html.push('="shell_win_all.account.');
				html.push(data.fun);
				html.push('(\'');
				html.push(data.id);
				html.push('\')">');
						
				html.push('<span class="lbl"></span></label>');
				
				html.push(data.name);
				html.push('</li>');
				
				return html.join("");
			},
			/**数据行触摸处理*/
			on_row_touch:function(id){
				shell_win.account[id].to_page();
			},
			/**更改开启登录状态*/
			on_change_login_status:function(id){
				var checked = $("#account_login_status")[0].checked;
				alert(checked);
//				Shell.util.Server.ajax({
//					url:Shell.util.Path.rootPath
//				},function(data){
//					if(!data.success){
//						alert(data.msg);
//					}
//				});
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
					
					html.push(shell_win.unit.get_input({
						id: "account_password_info_pwd_old",
						//iconCls: "glyphicon glyphicon-user",
						placeholder: "请输入原始密码",
						label:"原始密码"
					}));
					html.push(shell_win.unit.get_input({
						id: "account_password_info_pwd_new",
						//iconCls: "glyphicon glyphicon-user",
						placeholder: "请输入新密码",
						label:"新密码"
					}));
					html.push(shell_win.unit.get_input({
						id: "account_password_info_pwd_new2",
						//iconCls: "glyphicon glyphicon-user",
						placeholder: "请确认新密码",
						label:"确认密码"
					}));
					
					html.push('<button id="patient_info_submit_btn" class="btn btn-primary" style="width: 100%;margin-top:15px;" on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.account.password.submit();">确定</button>'); 
					
					return html.join("");
				},
				/**保存数据*/
				submit:function(){
					var pwd_old = $("#account_password_info_pwd_old").val(),
						pwd_new = $("#account_password_info_pwd_new").val(),
						pwd_new2 = $("#account_password_info_pwd_new2").val();
						
					if(!pwd_old || !pwd_new || !pwd_new2){
						alert("密码不能为空");
						return;
					}
					if(pwd_new != pwd_new2){
						alert("请确保新密码两次输入相同");
						return;
					}
					
					Shell.util.Server.ajax({
						url:Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/Capchcwoaduntnge?" + 
						"OldPwd=" + pwd_old + "&NewPwd=" + pwd_new
					},function(data){
						if(data.success){
							alert("密码修改成功");
							shell_win.page.back("#"+shell_win.page.lev["L2"].now,"#"+shell_win.page.lev["L2"].back);
						}else{
							data.msg = data.msg ? data.msg : "密码错误！";
							alert(data.msg);
						}
					});
				}
			}
		},
		/**页面*/
		page:{
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
				div.push('<span class="page_info_head_back" on' + Shell.util.Event.touch +
					'="shell_win_all.page.back(\'#' + shell_win.page.lev[L].now +
					'\',\'#' + shell_win.page.lev[L].back + '\');">< 返回</span>');

				if (title) {
					div.push('<span class="page_info_head_title">');
					div.push(title);
					div.push('</span>');
				}

				div.push('</div>');
				div.push('<div class="page_content" style="margin-bottom: 0;">');
				div.push(html);
				div.push('</div>');
				
				return div.join("");
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
				shell_win.util.mask.to_server("微信签名获取中...");
				Shell.util.Server.ajax({
					url: Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/GetJSAPISignature?" +
						"noncestr=" + shell_win.weixin.nonceStr + "&timestamp=" + shell_win.weixin.timestamp +
						"&url=" + shell_win.weixin.url
				}, function(data) {
					shell_win.util.mask.hide();
					if (data.success) {
						shell_win.weixin.signature = data.value.signature;
						shell_win.weixin.timeout = data.value.TimeOut;
						callback();
					} else {
						alert(shell_win.weixin.error_no_signature);
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
					alert(shell_win.weixin.error_no_signature);
				}
			}
		},
		/**初始化*/
		init:function(){
			//隐藏页面加载提示
			shell_win.util.mask.hide();
			//系统初始化
			shell_win.system.init();
			
			if(!shell_win.system.open_id){
				window.location.href = "login.html";
				return;
			}
			
			//首页初始化
			shell_win.home.init();
			//初始化页面动作
			shell_win.event.init();
			//微信功能初始化
			shell_win.weixin.init();
			//定位功能
			var arr = window.document.location.href.split("#");
			if(arr.length >= 2){
				shell_win.home.on_icon_touch(arr[1]);
			}
		}
	};
	//公开全局对象
	shell_win_all = shell_win;
	//初始化页面
	shell_win_all.init();
});