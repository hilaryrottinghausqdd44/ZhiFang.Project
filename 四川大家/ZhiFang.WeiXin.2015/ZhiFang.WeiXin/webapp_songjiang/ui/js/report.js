$(function() {
	//页面所有功能对象
	var shell_win = {
		/**系统*/
		system: {
			/**微信ID*/
			open_id: null,
			/**用户ID*/
			user_id: null,
			/**子账户ID*/
			member_id:null,
			/**报告ID*/
			report_id:null,
			/**系统初始化*/
			init: function() {
				shell_win.system.open_id = Shell.util.Cookie.getCookie("openId");
				shell_win.system.user_id = Shell.util.Cookie.getCookie("userId");
				
				var params = Shell.util.getRequestParams();
				shell_win.system.member_id = params.MemberId;
				shell_win.system.report_id = params.ReportId;
			}
		},
		/**报告查询*/
		report:{
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
					/**病历号*/
					patno:"patno",
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
					/**描述*/
					desc:"ResultDesc",
					/**结果状态*/
					status:"ResultStatus",
					/**单位*/
					unit: "Unit",
					/**参考值*/
					reference_value: "ReferenceValue"
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
				ShellComponent.mask.loading();
				Shell.util.Server.ajax({
					url:Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetSearchAccountReportFormById?" + 
						"ReportFormIndexId=" + shell_win.system.report_id + 
						"&SearchAccountId=" + shell_win.system.member_id
				},function(data){
					ShellComponent.mask.hide();
					var html = [];
					if(data.success){
						var list = data.value || [],
						len = list.length;
						for(var i=0;i<len;i++){
							html.push(shell_win.report.get_report_info_html(list[i]));
						}
						if(len == 0){
							html.push('<div style="text-align:center;padding:15px 5px;font-weight:bolder;">');
							html.push('不存在这份报告');
							html.push('</div>');
						}
					}else{
						html.push('<div style="text-align:center;padding:15px 5px;font-weight:bolder;">');
						html.push(data.msg);
						html.push('</div>');
					}
					
					$("#page_content").html(html.join(""));
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
//					div.push('<div class="report_info_data_div">检验单号：');
//					div.push(info[obj.info_obj.check_list_number]);
//					div.push('</div>');
					
					//病历号
					div.push('<div class="report_info_data_div">病历号：');
					div.push(info[obj.info_obj.patno]);
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
					//div.push('<div class="report_info_data_div">原始报告：');
					//div.push('<button onclick="shell_win_all.report.show_resouce_report_info();">原始报告</button>');
					//div.push('</div>');
					
					div.push('</div>');
					
					//项目内容列表
					var list = value[obj.item_list_field],
						len = list.length,
						list_table = [];
	
					list_table.push('<table class="report_items_table">');
					list_table.push('<thead><th>检验项目</th><th>结果</th><th>参考值</th></thead>');
					list_table.push('<tbody>');
					for (var i = 0; i < len; i++) {
						var row = list[i];
						list_table.push('<tr>');
						list_table.push('<td>');
						list_table.push(row[obj.item_obj.name]);
						list_table.push('</td>');
						
						list_table.push('<td>');
						list_table.push(row[obj.item_obj.result] || '');
						list_table.push(row[obj.item_obj.desc] || '');
						
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
						
						list_table.push(' ' + (row[obj.item_obj.unit] || ''));
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
					//div.push('<div class="report_info_data_div">原始报告：');
					//div.push('<button onclick="shell_win_all.report.show_resouce_report_info();">原始报告</button>');
					//div.push('</div>');
					
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
			
			if(!shell_win.system.member_id || !shell_win.system.report_id){
				var html = [];
				html.push('<div style="text-align:center;margin:20px;">');
				html.push('接收的参数错误<br/>');
				html.push('MemberId=' + shell_win.system.member_id + '<br/>');
				html.push('ReportId=' + shell_win.system.report_id + '<br/>');
				html.push('</div>');
				$("#page_content").html(html.join(""));
				return;
			}
			
			//加载数据
			shell_win.report.to_page();
		}
	};
	//初始化页面
	shell_win.init();
});