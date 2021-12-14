/**
 * 字典对照
 * @author GHX
 * @version 2021-04-07
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'layer', 'laydate', 'element', 'table', 'form'], function () {
	var $ = layui.jquery,
		layer = layui.layer,
		uxutil = layui.uxutil,
		table = layui.table,
		element = layui.element,
		laydate = layui.laydate,
		form = layui.form,
		$ = layui.jquery;
	var app = {
		IframeType:"",
		DicInfo:"",
		CName:"",
		SickTypeID:"",
		SickShortCode:"",
		SickTypeName:"",
		SickTableIndex:0,
		loadIndex:null,
		sickloadmsg:null
	};
	//table查询url
	app.TablerecordUrl = {
		"sicktable": ""
	};
	//初始排序
	app.tableInitialSort = {
		"table": []
	};
	app.url = {
		GET_ENUMURL: uxutil.path.ROOT +'/ServerWCF/CommonService.svc/GetClassDic',
		GET_SSICKTYPEURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeOtherDicContrastNum',
		GET_MAINTABLEURL:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBasicsDicAndLBDicCodeLink',
		ADDORUPDATE_LBITEMCODELINK:uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddOrUPDateLBDicCodeLink',
		DEL_LBTIMECODELINKURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBDicCodeLink',
		CopyLBItemCodeLinkContrast: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_CopyLBDicCodeLinkContrast',
	};
	/*-------初始化界面------*/
	app.init = function () {
		var me = this,
			params = uxutil.params.get(false);
		var loadmsg = layer.load();
		app.getContrastDicType(loadmsg);
		app.listener();		
	};	
	//获得字典类型下拉框信息
	app.getContrastDicType = function (loadmsg) {
		var me = this;
		var url = app.url.GET_ENUMURL +"?classnamespace=ZhiFang.Entity.LabStar&classname=ContrastDicType";
		uxutil.server.ajax({ url: url,async: false }, function (res) {
			if (res.success && res.ResultDataValue) {
				var data =  $.parseJSON(res.ResultDataValue);
				var value = data,
					html = '';
				$.each(value, function (i, item) {
					if(i==0){
						app.IframeType = item["Id"];
					}
					html += "<option value='" + item["Id"] + "' data-code='" + item["Code"] +"'>" + item["Name"]  + "</option>";
				});
				$("#ContrastDicType").html(html);
				form.render('select');
				app.initMainTable();
				layer.close(loadmsg);
				me.dicTypeChange();
			}
		});
	};	
	//初始化对接系统表格
	app.sickTypeTable = function (loadmsg) {
		var me = this;
		var url = app.url.GET_SSICKTYPEURL+"?ContrastDicType="+app.IframeType;
		me.sickloadmsg = layer.load();
		//初始化表格
		me.instance = table.render({
			elem: '#sicktable',
			height: "full-117",
			defaultToolbar: ['filter'],
			toolbar: false,
			size: 'sm', //小尺寸的表格
			autoSort: false, //禁用前端自动排序
			loading: false,
			page: false,
			totalRow: false, //开启合计行
			limit: 30,
			limits: [10, 30, 50, 100, 500],
			url: url,
			where: {
				time: new Date().getTime()
			},
			cols: [[
				{field: 'SickTypeID',width: 120,title: '索引',sort: false,hide: true},
				{field: 'CName',width: 82,title: '系统名称',sort: false,hide: false},
				{field: 'Shortcode',width: 60,title: '编码',sort: false,hide: false},
				{field: 'DicCNum',width: 80,title: '对照数量',sort: false,hide: false},
				{field: 'AllNum',width: 80,	title: '全部数量',sort: false,hide: false}
			]],
			text: {
				none: '暂无相关数据'
			},
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo || "", //解析提示文本
					"count": data.length || 0, //解析数据长度
					"data": data || []
				};
			},
			done: function (res, curr, count) {
				layer.close(me.sickloadmsg);
				if (count == 0) {
				    return;
				};
				if (app.SickTableIndex >= 0) {
					var index = app.SickTableIndex + 1;
					$("#sicktable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").click();
					if (document.querySelector("#sicktable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")")) {
						document.querySelector("#sicktable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
					}
					//app.SickTableIndex = null;
				} else {
					//选中第一条
					if($("#sicktable+div .layui-table-body table.layui-table tbody tr:first-child").length > 0)
						$("#sicktable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
				} 
			}
		});
	};
	//初始化主表格
	app.initMainTable = function (url) {
		var me = this,
			url = url || '';
					
		//初始化表格
		me.instance = table.render({
			elem: '#mainTable',
			height: "full-140",
			defaultToolbar: ['filter'],
			toolbar: '#Toolbar',
			size: 'sm', //小尺寸的表格
			autoSort: false, //禁用前端自动排序
			loading: false,
			page: true,
			totalRow: false, //开启合计行
			limit: 30,
			limits: [10, 30, 50, 100, 500],
			url: url,
			where: {
				time: new Date().getTime()
			},
			cols: [[
				{type: 'checkbox', fixed: 'left'},
				{field: 'LabID',minWidth: 120,title: 'LabID',	sort: false,	hide: true},
				{field: 'DicDataID',minWidth: 120,title: '项目索引',	sort: false,	hide: true},
				{field: 'Id',minWidth: 120,title: '映射关系ID',	sort: false,	hide: true},
				{field: 'DicDataName',minWidth: 120,title: '名称',sort: false,hide: false},
				{field: 'DicDataCode',minWidth: 100,title: '编码',sort: false,hide: false},
				{field: 'LinkDicDataCode',minWidth: 100,title: '对照编码',edit:'text',sort: false,hide: false},
				{field: 'LinkDicDataName',minWidth: 120,title: '对照名称',edit:'text',sort: false,hide: false},
				{field: 'DicTypeCode',minWidth: 120,title: '字典类型编码',edit:'text',sort: false,hide: true},
				{field: 'DicTypeName',minWidth: 120,title: '字典类型名称',edit:'text',sort: false,hide: true},
				{field: 'TransTypeID',minWidth: 100,title: '对照类型',sort: false,hide: false,templet: '#TransTypeTool'},
				{field: 'DispOrder',minWidth: 100,title: '显示顺序',sort: false,hide: true},
				{field: 'Tab', minWidth: 100, title: '用于判断行是否有修改数据', hide: true, sort: false }
			]],
			text: {
				none: '暂无相关数据'
			},
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo || "", //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list|| []
				};
			},
			done: function (res, curr, count) {
				if(app.loadIndex) layer.close(app.loadIndex);
				if(count == 0) {
					return;
				};
				$("select[name='TransTypeID']").parent('div.layui-table-cell').css('overflow', 'visible');
				$("select[name='TransTypeID']").parent('div.layui-table-cell').css('padding', '0 15px');
				//匹配质控类型下拉数据
				var that = this.elem.next();
				for(var i = 0; i < res.data.length; i++) {
					var QCType = res.data[i].TransTypeID;
					var trRow = that.find(".layui-table-box tbody tr[data-index='" + i + "']");
					$(trRow).find("td").each(function() {
						var fieldName = $(this).attr("data-field");
						var selectJq = $(this).find("select");
						//选中值
						if(selectJq.length == 1) {
							if(QCType == res.data[i][fieldName]) {
								$(this).children().children().val(QCType == null ? 0 : QCType);
							}
						}
					});
				}
				form.render('select');
				$("select[name='TransTypeID']+div.layui-form-select").find("input").css('height', '30px'); //小表格存在下拉框时设置
				var newarr = [];
				for (var i = res.data.length - 1; i >= 0; i--) {
					var targetNode = res.data[i].LinkDicDataCode;
					if (targetNode) {
						for (var j = 0; j < i; j++) {
							if (targetNode == res.data[j].LinkDicDataCode) {
								var ispush = true;
								if (newarr.length > 0) {
									for (var a = 0; a < newarr.length; a++) {
										if (newarr[a] == targetNode) {
											ispush = false;
										}
									}
								}
								if (ispush) {
									newarr.push(targetNode);
								}
								var trRow = that.find(".layui-table-box tbody tr[data-index='" + i + "']");
								$(trRow).css("background-color", "#8EC2F5");
								var dtrRow = that.find(".layui-table-box tbody tr[data-index='" + j + "']");
								$(dtrRow).css("background-color", "#8EC2F5");
							}
						}
					}
				}
				if (newarr.length > 0) {
					$("#talbetooltext").html("&nbsp;&nbsp;重复配置:" + newarr.join(","));
				}
			}
		});
	};
	//加载表格
	app.loadsicktypetable = function(){
		var sectionid = app.SectionID != 0 ? app.SectionID : 0,
			grouptype = app.GroupType != 0 ? app.GroupType : 0,
			itemcname = app.ItemCName ? app.ItemCName : "";
		app.TablerecordUrl["sicktable"] = `${app.url.GET_SSICKTYPEURL}?ContrastDicType=${app.IframeType}`;
		app.sickloadmsg = layer.load();
		table.reload('sicktable', {
			url: app.TablerecordUrl["sicktable"],
			where: {
				time: new Date().getTime()
			}/*,done: function (res, curr, count) {
				layer.close(loadIndex);
				if (count == 0) {
				    return;
				};
				//选中第一条
				if($("#sicktable+div .layui-table-body table.layui-table tbody tr:first-child").length > 0)
					$("#sicktable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
			}*/
		}); 
	};
	/*----------监听事件---------*/
	app.listener = function () {
		var me = this;
		form.on('select(ContrastDicType)', function (data) {
			$("#CName").val("");
			app.CName = "";
			app.dicTypeChange();
		});
		form.on('select(SType)', function (data) {
			var option = $("#SType option:selected"),
				Value = option.val();
			me.DicInfo = Value;
			me.loadtable();
		});
		//对接类型选择
		form.on('select(TransTypeID)', function (data) {
		    //这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
		    var elem = data.othis.parents('tr');
		    var dataindex = elem[0].dataset.index;
		    var tableCache = table.cache["mainTable"];
		
		    //改变后的数据
		    var rowObj = tableCache[dataindex].Tab;
		    if (rowObj) delete rowObj.TransTypeID;
		    if (!rowObj) rowObj = {};
		    $.each(tableCache, function (index, value) {
		        if (index == dataindex) {
		            if (data.value) rowObj.TransTypeID = data.value;
		            value.Tab = rowObj;
		            value.TransTypeID = data.value;
		        }
		    });
		});
		//监听表格工具栏
		table.on('toolbar(mainTable)', function (obj) {
		    var layEvent = obj.event;
		    if (layEvent == 'add') {//新增
		        app.addClick();
		    } else if (layEvent === 'del') { //删除
		       app.delClick();
		    }else if (layEvent === 'edit') { //编辑
				app.editClick();
			} else if (layEvent === 'copy') {//复制
				app.copyClick();
			}
		});
		//监听单元格编辑 编辑以后给列打上标记
		table.on('edit(mainTable)', function (obj) {
		    var value = obj.value, //得到修改后的值
		        data = obj.data,//得到所在行所有键值
		        field = obj.field; //得到字段
		    var tableCache = table.cache["mainTable"];
		    var dataindex = obj.tr[0].dataset.index;
		    tableCache[dataindex].Tab = true;
		});
		//监听行单击事件
		table.on('row(sicktable)', function (obj) {
		    //标注选中样式
		    //obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			$(".layui-table-body tr ").attr({ "style": "background:#FFFFFF" });
			$(obj.tr.selector).attr({ "style": "background:#77DDFF" });
			app.SickTypeID = obj.data.SickTypeID;
			app.SickTypeName = obj.data.CName;
			app.SickShortCode = obj.data.Shortcode;
			app.SickTableIndex = Number(obj.tr[0].dataset.index);
			me.loadtable();
		});
		$("#search").click(function(){
			window.event.preventDefault();
			app.CName = $("#CName").val();
			app.loadtable();		
			return false;
		});
		//监听项目名称输入框回车事件
		$(document).ready(  
			function() {  
				$("#CName").keydown(function(event) {  
					if (event.keyCode == 13) {  
						app.CName = $("#CName").val();
						app.loadtable();
					}  
				})  
			}  
		);
	};
	app.loadtable = function(){
		var cname = app.CName != "" ? app.CName : "",
			contrastdictype = app.IframeType ? app.IframeType : 0,
			dicinfo = app.DicInfo ? app.DicInfo : "",
			sicktypeid = app.SickTypeID != 0 ? app.SickTypeID : "";
		app.TablerecordUrl["table"] = `${app.url.GET_MAINTABLEURL}?SickTypeId=${sicktypeid}&ContrastDicType=${contrastdictype}&DicInfo=${dicinfo}&CName=${cname}`;
		app.loadIndex = layer.load();
		table.reload('mainTable', {
			url: app.TablerecordUrl["table"],
			where: {
				time: new Date().getTime()
			}
		});
	};
	//字典变化处理
	app.dicTypeChange = function () {
		var me = this,
			option = $("#ContrastDicType option:selected"),
			Value = option.val(),
			Code = option.attr("data-code");
		switch (Value) {
			case "1":
				me.loadSTypeSelect(1);
				break;
			case "2":
				if ($(".my_hide").hasClass("layui-hide")) $(".my_hide").removeClass("layui-hide");
				me.loadSTypeSelect(2);
				break;
			default:
				if (!$(".my_hide").hasClass("layui-hide")) $(".my_hide").addClass("layui-hide");
				break;
		}
		me.IframeType = Value;
		//me.ContrastDicType = Value;
		if (table.cache["sicktable"])
			app.loadsicktypetable();
		else
			app.sickTypeTable();		
	};
	//人员，部门特有下拉框处理
	app.loadSTypeSelect = function (type) {
		var me = this;
		if(type+"" == "1"){
			var url = app.url.GET_ENUMURL +"?classnamespace=ZhiFang.Entity.LabStar&classname=EmpSystemType";
			uxutil.server.ajax({ url: url,async: false }, function (res) {
				if (res.success && res.ResultDataValue) {
					var data =  $.parseJSON(res.ResultDataValue);
					var value = data,
						html = '<option value="1">全部</option>';
					$.each(value, function (i, item) {
						html += "<option value='" + item["Id"] +"'>" + item["Name"]  + "</option>";
					});
					$("#SType").html(html);
					form.render('select');
					me.stypeChange();
				}
			});
			
		}else{
			var url = app.url.GET_ENUMURL +"?classnamespace=ZhiFang.Entity.LabStar&classname=DeptSystemType";
			uxutil.server.ajax({ url: url,async: false }, function (res) {
				if (res.success && res.ResultDataValue) {
					var data =  $.parseJSON(res.ResultDataValue);
					var value = data,
						html = '<option value="1">全部</option>';
					$.each(value, function (i, item) {
						html += "<option value='" + item["Id"]+ "'>" + item["Name"]  + "</option>";
					});
					$("#SType").html(html);
					form.render('select');
					me.stypeChange();
				}
			});
		}
	};
	//角色下拉框变化处理
	app.stypeChange = function () {
		var me = this,
			option = $("#SType option:selected"),
			Value = option.val();
		me.DicInfo = Value;
		//me.loadtable();
	};
	//新增
	app.addClick = function () {
		var tableCache = table.cache["mainTable"];
		if (!tableCache || tableCache.length == 0) {
		    layer.msg("没有检验项目无法对照！");
		    return;
		}
		var cname = app.CName != "" ? app.CName : "",
			contrastdictype = app.IframeType ? app.IframeType : 0,
			dicinfo = app.DicInfo ? app.DicInfo : "",
			sicktypeid = app.SickTypeID != 0 ? app.SickTypeID : "";
	    layer.open({
	        type: 2,
	        btn: ['保存'],
	        title: ['追加对照', 'font-weight:bold;'],
	        skin: 'layui-layer-lan',
	        area: ['30%', '50%'],
	        content: `${uxutil.path.ROOT}/ui/layui/app/dic/dic_contrast/adddicc/addcontrast.html?SickTypeId=${sicktypeid}&ContrastDicType=${contrastdictype}&DicInfo=${dicinfo}&CName=${cname}`,
	        yes: function (index, layero) {
	            var iframeWin = window[layero.find('iframe')[0]['name']],
					LinkDicDataName = $(iframeWin.document).find("#LinkDicDataName").val(),
					LinkDicDataCode = $(iframeWin.document).find("#LinkDicDataCode").val(),
					TransTypeID = $(iframeWin.document).find("#TransTypeID").val(),
					ItemID =  $(iframeWin.document).find("#ItemID").val();
					
				if(!LinkDicDataCode){
					layer.msg("编码不可为空！");
					return; 
				}
				var loadIndex = layer.load();
				var iteminfo = null;
				for (var i = 0;i<iframeWin.ITEMS.length;i++) {
					if(ItemID == iframeWin.ITEMS[i].DicDataID){
						iteminfo = iframeWin.ITEMS[i];
					}
				}
				var tableCache = table.cache["mainTable"];
				for(var a=0;a<tableCache.length;a++){
					if(tableCache[a].DicDataID == iteminfo.DicDataID && tableCache[a].LinkDicDataCode == LinkDicDataCode){
						layer.close(loadIndex);
						layer.msg("同项目对照编码不可重复！");
						return; 
					}
				}
				var entity = {
					LabID:0,
					Id: 0,
					LinkSystemCode: app.SickShortCode,//对接系统编码
					LinkSystemName: app.SickTypeName,//对接系统名称
					LinkSystemID: app.SickTypeID,//对接系统ID
					DicDataID:iteminfo.DicDataID,
					DicDataCode:iteminfo.DicDataCode,
					DicDataName:iteminfo.DicDataName,
					DicType:app.IframeType,
					LinkDicDataCode:LinkDicDataCode,
					LinkDicDataName:LinkDicDataName,
					TransTypeID:TransTypeID ? TransTypeID : 0,
					DispOrder:iteminfo.DispOrder,
					IsUse: 1
				};
				var configs = {
				    type: "POST",
				    url: app.url.ADDORUPDATE_LBITEMCODELINK,
				    data: JSON.stringify({entity:entity})
				};
				uxutil.server.ajax(configs, function (data) {
				    if (data.success) {
				        layer.msg("保存成功!", { time: 3000 });
						layer.close(index);
						table.reload("sicktable", {});
				        layer.close(loadIndex);
						
				    }else{
						layer.msg(data.ErrorInfo, { time: 3000 });
						layer.close(loadIndex);
					}
				});
	        },
	        success: function (layero, index) {
	        }
	    });
	};
	//编辑
	app.editClick = function () {
	    var tableCache = table.cache["mainTable"],
	        arr = [],
			delarr = [];
	    if (!tableCache || tableCache.length == 0) {
	        layer.msg("没有修改数据不需要保存！");
	        return;
	    }
	    $.each(tableCache, function (i, item) {
	        if (item.Tab && item.LinkDicDataCode != "" && item.LinkDicDataCode != null) {
				arr.push(item);
			}else if(item.Tab && item.Id != "0" && (item.LinkDicDataCode == "" || item.LinkDicDataCode == null)){
				delarr.push(item);
			}
	    });
		for(var i=0;i<arr.length;i++){
			for(var a=0;a<tableCache.length;a++){
				if(tableCache[a].Id != arr[i].Id && tableCache[a].DicDataID == arr[i].DicDataID && tableCache[a].LinkDicDataCode == arr[i].LinkDicDataCode){
					layer.msg("同项目对照编码不可重复！");
					return; 
				}
			}
		} 
	    var len = arr.length,
			dellen = delarr.length,
	        successCount = 0,
			delCount = 0,
	        failCount = 0;
	    if ((len+dellen) == 0) {
	        layer.msg("没有修改数据不需要保存！");
	        return;
	    }
	    var loadIndex = layer.load();
		//新增或者修改
	    $.each(arr, function (i, item) {
	        var entity = {
				LabID:item.LabID,
				Id: item.Id,
				LinkSystemCode: app.SickShortCode,//对接系统编码
				LinkSystemName: app.SickTypeName,//对接系统名称
				LinkSystemID: app.SickTypeID,//对接系统ID
				DicDataID:item.DicDataID,
				DicDataCode:item.DicDataCode,
				DicDataName:item.DicDataName,
				DicType:app.IframeType,
				LinkDicDataCode:item.LinkDicDataCode,
				LinkDicDataName:item.LinkDicDataName,
				TransTypeID:item.TransTypeID ? item.TransTypeID : 0,
				DispOrder:item.DispOrder,
				IsUse: 1
	        };
	        var configs = {
	            type: "POST",
	            url: app.url.ADDORUPDATE_LBITEMCODELINK,
	            data: JSON.stringify({entity:entity})
	        };
	        uxutil.server.ajax(configs, function (data) {
	            if (data.success) {
	                successCount++;
	            } else {
	                failCount++;
	            }
	            if (successCount + delCount + failCount == len+dellen) {
	                table.reload("sicktable", {});
	                layer.msg("保存或修改：" + successCount + "个，删除："+delCount+"，失败：" + failCount + "个", { time: 3000 });
	                layer.close(loadIndex);
	            }
	        });
	    });
		//删除编码为空的对照关系
		$.each(delarr, function (i, item) {
		    var url = app.url.DEL_LBTIMECODELINKURL + '?id=' + item.Id;
		    uxutil.server.ajax({
		        url: url
		    }, function (data) {
		        if (data.success) {
		            delCount++;
		        } else {
		            failCount++;
		        }
		        if (successCount + delCount + failCount == len+dellen) {
		            table.reload("sicktable", {});
		            layer.msg("保存或修改：" + successCount + "个，删除："+delCount+"，失败：" + failCount + "个", { time: 3000 });
		            layer.close(loadIndex);
		        }
		    });
		});
	};
	//删除
	app.delClick = function () {
	    var checkData = table.checkStatus("mainTable").data;
	    if (checkData && checkData.length == 0) {
	        layer.msg("请选择需要删除的数据！");
	        return;
	    }
		for(var i=0;i<checkData.length;i++){
			if(checkData[i].Id == 0 || checkData[i].Id == "0" || checkData[i].LinkDicDataCode == "" || checkData[i].LinkDicDataCode == null){
				layer.msg("未对照的数据不可删除！");
				return;
			}
		}
	    var len = checkData.length,
	        successCount = 0,
	        failCount = 0;
	    layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
	        var indexs = layer.load();//显示遮罩层
	        $.each(checkData, function (i, item) {
	            var url = app.url.DEL_LBTIMECODELINKURL + '?id=' + item.Id;
	            uxutil.server.ajax({
	                url: url
	            }, function (data) {
	                if (data.success === true) {
	                    successCount++;
	                } else {
	                    failCount++;
	                }
	                if (successCount + failCount == len) {
	                    table.reload("sicktable", {});
	                    layer.close(indexs);
	                    layer.close(index);
	                    layer.msg("删除完毕！成功：" + successCount + "个，失败：" + failCount + "个", { time: 3000 });
	                }
	            });
	        });
	    });
	};
	//复制对照
	app.copyClick = function () {
		var me = this;
		if (!me.SickTypeID) {
			layer.msg("未选择对接系统！");
			return;
		} else if (!me.IframeType) {
			layer.msg("请选择字典类型！");
			return;
		} else {
			layer.open({
				type: 2,
				btn: ['复制'],
				title: ['复制对照', 'font-weight:bold;'],
				skin: 'layui-layer-lan',
				area: ['40%', '60%'],
				content: uxutil.path.ROOT + '/ui/layui/app/dic/dic_contrast/item/copyItemc/copyContrast.html?SickTypeID=' + app.SickTypeID,
				yes: function (index, layero) {
					var iframeWin = window[layero.find('iframe')[0]['name']];
					var dataarr = iframeWin.layui.table.checkStatus('copytable').data;
					if (dataarr.length < 1) {
						layer.msg("请选择要复制的对接系统！");
						return;
					}
					var sicktypeidarr = [];
					for (var i = 0; i < dataarr.length; i++) {
						sicktypeidarr.push(dataarr[i].LBSickType_Id);
					}
					var loadIndex = layer.load();
					var configs = {
						type: "GET",
						url: app.url.CopyLBItemCodeLinkContrast + '?SickTypeIds=' + sicktypeidarr.join(",") + '&thisSickTypeId=' + app.SickTypeID+'&dictype='+me.IframeType
					};
					uxutil.server.ajax(configs, function (data) {
						if (data.success) {
							layer.msg("复制成功!", { time: 3000 });
							layer.close(index);
							table.reload("sicktable", {});
							layer.close(loadIndex);

						} else {
							layer.msg(data.ErrorInfo, { time: 3000 });
							layer.close(loadIndex);
						}
					});
				},
				success: function (layero, index) {
				}
			});

		}
	};
	app.init();
});
