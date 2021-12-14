/**
	@name：模块表单表格配置应用关系
	@author：guohx
	@version 2020-07-29
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form;

    var config = {
    	systemHost: '',
    	ModuleList: [] 
    };

    var mainTable = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
        config: {
        	PK: null,
            elem: '',
            id: "",
            checkRowData: [],
            /**默认传入参数*/
            defaultParams: {},
            /**默认数据条件*/
            defaultWhere: '',
            /**内部数据条件*/
            internalWhere: '',
            /**外部数据条件*/
            externalWhere: '',
            /**是否默认加载*/
            defaultLoad: false,
            /**列表当前排序*/
           // 该集合现在不支持排序
            /*sort: [{
                "property": 'DispOrder',
                "direction": 'ASC'
            }],*/
			
//          selectUrl: uxutil.path.ROOT + '/ServerWCF/ModuleConfigService.svc/ST_UDTO_SearchBModuleModuleFormGridLinkByHQL?isPlanish=true',
//          addUrl: uxutil.path.ROOT + '/ServerWCF/ModuleConfigService.svc/ST_UDTO_AddBModuleModuleFormGridLink',
//          editUrl: uxutil.path.ROOT + '/ServerWCF/ModuleConfigService.svc/ST_UDTO_UpdateBModuleModuleFormGridLinkByField',
//          delUrl: uxutil.path.ROOT + '/ServerWCF/ModuleConfigService.svc/ST_UDTO_DelBModuleModuleFormGridLink',
			// 升级系统配置服务
			upgradeSystemConfigUrl: '/ServerWCF/ModuleConfigService.svc/UpdateModuleConfigDefault',
			// 展示每个模块配置的各种集合（grid.form.chart）
            selectUrl:'/ServerWCF/ModuleConfigService.svc/SearchModuleAggregateList',
            //新增集合
            addFormListUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_AddBModuleFormList',
            addGridListUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_AddBModuleGridList',
//          addChartListUrl: uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_AddBModuleChartList',
            //编辑集合
            editFormListUrl:  '/ServerWCF/ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormListByField',
            editGridListUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_UpdateBModuleGridListByField',
//          editChartListUrl: uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBModuleChartListByField',
            //根据Id获得具体集合信息
            getFormListUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_SearchBModuleFormListById',
            getGridListUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_SearchBModuleGridListById',
//          getChartListUrl: uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBModuleChartListById',
            where: "",
//          toolbar: '#Toolbar',
//          toolbar: '',
            defaultToolbar: ['filter'],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 300, 500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [],
            text: { none: '暂无相关数据' },
            response: function () {
                return {
                    statusCode: true, //成功状态码
                    statusName: 'code', //code key
                    msgName: 'msg ', //msg key
                    dataName: 'data' //data key
                }
            },
            parseData: function (res) {//res即为原始返回的数据
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n")) : {};
                // 增加一个typeList字段：可取值：GridCode，FormCode，ChartCode
                var helper = {
                	grid: 'GridCode',
                	form: 'FormCode',
                	chart: 'ChartCode'
                };
                data.forEach(function(obj,i,arr){
                	obj.typeList = helper[obj.AggregateType];
                });
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.length || 0, //解析数据长度
                    "data": data || []
                };
            },
            done: function (res, curr, count) {
                //无数据处理
                if (count == 0) return;
                //默认选择第一行
                var rowIndex = 0;
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].Id == mainTable.config.PK) {
                        rowIndex = res.data[i].LAY_TABLE_INDEX;
                        break;
                    }
                }
                doAutoSelect(mainTable.config, rowIndex);
                //触发点击事件
//              $("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();

            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //构造器
    var Class = function (options) {
        var me = this;
        me.config = $.extend({}, mainTable.config, me.config, options);
        if (me.config.defaultLoad) me.config.url = me.getLoadUrl();
        me = $.extend(true, {}, mainTable, Class.pt, me);// table,
        return me;
    };
    Class.pt = Class.prototype;
    //获取查询Url
    Class.pt.getLoadUrl = function () {
        var me = this,
        	arr = [],
        	url = (config.systemHost || uxutil.path.ROOT) + me.config.selectUrl;
        var codeObj = me.getConfigCodes();
        if(codeObj){
        	if(codeObj.gridCodeList.length){
          		url += (url.indexOf('?') == -1 ? '?' : '&') + 'GridCodes=' + codeObj.gridCodeList;
        	}
        	if(codeObj.formCodeList.length){
          		url += (url.indexOf('?') == -1 ? '?' : '&') + 'FormCodes=' + codeObj.formCodeList;
        	}
        	if(codeObj.chartCodeList.length){
          		url += (url.indexOf('?') == -1 ? '?' : '&') + 'CheartCodes=' + codeObj.chartCodeList;
        	}
        }
        //默认条件
        if (me.config.defaultWhere && me.config.defaultWhere != '') {
            arr.push(me.config.defaultWhere);
        }
        //内部条件
        if (me.config.internalWhere && me.config.internalWhere != '') {
            arr.push(me.config.config.internalWhere);
        }
        //外部条件
        if (me.config.externalWhere && me.config.externalWhere != '') {
            arr.push(me.config.externalWhere);
        }
        //传入的默认参数处理
        if (me.config.defaultParams) {

        }
//      if (config.ModuleID) arr.push("ModuleID='" + config.ModuleID + "'");
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + where;
        }
        var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        if (defaultOrderBy && defaultOrderBy.length > 0) url += '&sort=' + JSON.stringify(defaultOrderBy);

        return url;
    };
    // 将模块配置的数据进行重建
    Class.pt.getConfigCodes = function(){
    	var me = this;
		// 将模块的每种配置都加载出来
		if(config.ModuleList.length) {
			var gridCodeList = [],
				formCodeList = [],
				chartCodeList = [];
			config.ModuleList.forEach(function(item,index,arr){
				var configObj = item.list;
				$.each(configObj.GridCode, function(index,item) {
					if(item.code) {
						gridCodeList.push(item.code);
					}
				});
				$.each(configObj.FormCode, function(index,item) {
					if(item.code) {
						formCodeList.push(item.code);
					}
				});
				$.each(configObj.chartCode, function(index,item) {
					if(item.code) {
						chartCodeList.push(item.code);
					}
				});
			});
			return {
				gridCodeList: gridCodeList,
				formCodeList: formCodeList,
				chartCodeList: chartCodeList
			}

    	}
    	return false;
    }
    //获取列
    Class.pt.getCols = function (role) {
        var me = this,
            role = role || '',
            cols = [];
        cols = [
            { type: 'checkbox' },
            { type: 'numbers', title: '行号' },
            { field: 'Id', width: 80, title: '主键ID（各种集合的id）', sort: false, hide: true },
            { field: 'typeList', title: '集合类型', width: 140, hide: true},
            { field: 'Name', title: '名称', width: 140, sort: true },
            { field: 'Code', title: '代码', width: 140, sort: true},
            { field: 'TypeName', title: '类型名称', width: 140, sort: true ,hide: true},
            { field: 'ClassName', title: '样式类型名称', width: 140, sort: true, hide: true },
            {
                field: 'AggregateType', title: '类型', width: 120, sort: true,
                templet: function (data) {
                    var str = "";
                    if (data.AggregateType === 'form')
                        str = "<span style='color:green;'>表单配置</span>";
                    else if (data.AggregateType === 'grid')
                        str = "<span style='color:red;'>表格配置</span>";
                    else if (data.AggregateType === 'chart')
                        str = "<span style='color:blue;'>图表配置</span>";
                    return str;
                }
            },
            {
                field: 'IsUse', title: '是否使用', width: 120, sort: true,
                templet: function (data) {
                    var str = "";
                    if (data.IsUse === "True")
                        str = "<span style='color:green;'>是</span>";
                    else
                        str = "<span style='color:red;'>否</span>";
                    return str;
                }
            },
            { field: 'DispOrder', title: '显示次序', width: 120, sort: true, hide: true },
//          { title: '操作', width: 260, align: 'center', toolbar: '#tableOperation', fixed: 'right'}
            { field: 'Id',title: '操作', width: 260, align: 'center', templet: function(data){
            	if(role === 'system') { // 开发
            		if(data.Id){ // 修改+设置
	            		var str = '<a class="layui-btn layui-btn-xs" lay-event="set" style="line-height: 18px;">设置配置项</a>' +
	            		 '<a class="layui-btn layui-btn-xs" lay-event="editList" style="line-height: 18px;">修改集合</a>';
	        			
	            	}else { // 新增
	            		var str = '<a class="layui-btn layui-btn-xs" lay-event="addList" style="line-height: 18px;">新增集合</a>' ;

	            	}
            	}else { // 用户
            		if(data.Id){
            			
            			var str = '<a class="layui-btn layui-btn-xs" lay-event="set" style="line-height: 18px;">设置配置项</a>';
            		}else {
            			var str = '';
            		}
            		
            	}
            	return str;
            	
            }, fixed: 'right'}
        ];
        return [cols];
    };
    // 获取查询Fields---可以舍弃
    Class.pt.getStoreFields = function (isString) {
        var me = this,
            columns = me.config.cols[0] || [],
            length = columns.length,
            fields = [];
        for (var i = 0; i < length; i++) {
            if (columns[i].field) {
                var obj = isString ? columns[i].field : {
                    name: columns[i].field,
                    type: columns[i].type ? columns[i].type : 'string'
                };
                fields.push(obj);
            }
        }
        return fields;
    };
    //核心入口
    mainTable.render = function (options) {
        var me = this;
        var inst = new Class(options);
        inst.config.cols = Class.pt.getCols(options.role);
        me.config.cols = Class.pt.getCols(options.role);
        inst.tableIns = table.render(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    mainTable.onSearch = function (mytable, ModuleList,systemHost) {
        var me = this;
        config.ModuleList = ModuleList;
        // 更新集合的url
        config.systemHost = uxutil.path.LOCAL + '/' + systemHost;
        var inst = new Class(me);
        mainTable.url = inst.getLoadUrl();
        mainTable.config.elem = "#" + mytable;
        mainTable.config.id = mytable;
        table.reload(mytable, {
            url: inst.getLoadUrl()
        });
    };
    //监听
    Class.pt.iniListeners = function () {
        var me = this;
        //监听表格头工具栏---增加了升级系统配置事件
        table.on('toolbar(mainTable)', function (obj) {
            var layEvent = obj.event,
                checkData = table.checkStatus(me.config.id).data;
            switch (layEvent) {
            	case "upgradeSystemConfig": // 升级系统配置
                    me.upgradeSystemConfig();
                    break;
                case "addList"://新增集合
                    me.addListClick();
                    break;
                case "editList"://修改集合
                    if (checkData && checkData.length != 1)
                        layer.msg("请勾选一条数据进行该集合修改！");
                    else
                        me.editListClick(checkData[0]);
                    break;
                case "add"://新增配置项
                    if (config.ModuleID) 
                        me.addClick();
                    else
                        layer.msg("请选择模块节点!");
                    break;
                case "edit"://修改勾选配置项
                    if (checkData && checkData.length != 1) 
                        layer.msg("请勾选一条数据进行修改！");
                    else
                        me.editClick(checkData[0]);
                    break;
                case "del"://删除勾选配置项
                    if (checkData && checkData.length == 0) 
                        layer.msg("请勾选需要删除的数据！");
                    else
                        me.delClick(checkData);
                    break;

            }
        });
        //监听表格行工具事件（重要）
        table.on('tool(mainTable)', function (obj) {
            var data = obj.data, //获得当前行数据
                layEvent = obj.event;
            if(me.config.role === 'system'){ // 开发
            	if (layEvent === 'addList') { 
	                me.addListClick(data);
	            }else if(layEvent === 'editList'){
	            	me.editListClick(data);
	            }
            }
            // 设置配置项(用户和开发)
            if(layEvent === 'set'){
            	me.setConfigItem(data);
            }
        });
        //监听行双击事件
        table.on('rowDouble(mainTable)', function (obj) {
            var data = obj.data; //获得当前行数据
            me.setConfigItem(data);
        });
        //监听行单击事件
        table.on('row(mainTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        });
        //监听排序事件
        table.on('sort(mainTable)', function (obj) {
            var field = obj.field;//排序字段
            var type = obj.type;//升序还是降序
            if (!table.cache[me.config.id] || table.cache[me.config.id].length == 0) return;
            //修改默认的排序字段
            me.config.sort = [{
                "property": field,
                "direction": type
            }];
            table.reload('mainTable', {
                initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
                url: me.getLoadUrl(),
                where: {
                    time: new Date().getTime()
                }
            });
        });
        //监听集合类型下拉框
        form.on('select(addListModalFormListType)', function (data) {
            var value = data.value; //得到被选中的值
            if (value == "FormCode") {
                $("#addListModalFormTypeID").html('<option value="">请选择</option><option value = "1" >查询</option ><option value="2">编辑</option>');
                $("#addListModalFormClassID").html('<option value="">请选择</option><option value = "1" >普通</option ><option value="2">收缩</option>');
            } else if (value == "GridCode") {
                $("#addListModalFormTypeID").html('<option value="">请选择</option><option value = "1" >普通分页</option ><option value="2">合计</option><option value="3">固定列</option>');
                $("#addListModalFormClassID").html('<option value="">请选择</option>');
            }
            form.render('select');
        });  
    };
    // 升级系统配置
    Class.pt.upgradeSystemConfig = function(){
    	var me = this;
    	var url = (config.systemHost || uxutil.path.ROOT) + me.config.upgradeSystemConfigUrl;
    	var indexs = layer.load();
    	uxutil.server.ajax({
    		url: url
    	},function(data){
    		 //隐藏遮罩层
		    layer.close(indexs);
		    if (data.success) {
		        layer.msg("升级成功!", { icon: 6, anim: 0, time: 3000 });
		    } else {
		        layer.msg("升级失败！", { icon: 5, anim: 6 });
		    }
    	})
    	
    }
    //新增集合
    Class.pt.addListClick = function (data) {
        var me = this;
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['新增集合'],
            skin: 'IEQA-class',
            area: ['60%', '65%'],
            content: $('#addListModal'),
         	success: function(layero, index){
		    	// code和类型赋值
		    	$("#addListModalFormCode").val(data.Code);
		    	$("#addListModalFormListType").val(data.typeList);
		    	form.render('select');
		  	},
            yes: function (index, layero) {
                var entity = {};
                entity["LabID"] = uxutil.cookie.get('LABID') || 0;
//              entity["LabNo"] = $("#addListModalFormLabID").val() ? $("#addListModalFormLabID option:selected").attr("data-labno") : "";
                entity[$("#addListModalFormListType").val()] = data.Code;
                entity["CName"] = $("#addListModalFormCName").val();
                entity["ShortCode"] = $("#addListModalFormShortCode").val();
                entity["ShortName"] = $("#addListModalFormShortName").val();
                entity["TypeID"] = $("#addListModalFormTypeID").val() ? $("#addListModalFormTypeID").val() : 0;
                entity["TypeName"] = $("#addListModalFormTypeID").val() ? $("#addListModalFormTypeID option:selected").text() : "";
                entity["ClassID"] = $("#addListModalFormClassID").val() ? $("#addListModalFormClassID").val() : 0;
                entity["ClassName"] = $("#addListModalFormClassID").val() ? $("#addListModalFormClassID option:selected").text() : "";
                entity["SourceCodeUrl"] = $("#SourceCodeUrl").val();
                entity["SourceCode"] = $("#SourceCode").val();
                entity["IsUse"] = $("#addListModalFormIsUse").prop("checked") ? 1 : 0;
                entity["DispOrder"] = ($("#addListModalFormDispOrder").val() && !isNaN($("#addListModalFormDispOrder").val())) ? $("#addListModalFormDispOrder").val() : 0;
                var url = "";
                switch (data.typeList) {
                    case "FormCode":
                        url = config.systemHost + me.config.addFormListUrl;
                        break;
                    case "GridCode":
                        url = config.systemHost + me.config.addGridListUrl;
                        break;
                    case "ChartCode":
                        url = config.systemHost + me.config.addChartListUrl;
                        break;
                    default:
                        url = config.systemHost + me.config.addFormListUrl;
                        break;
                }
                //显示遮罩层
                var indexs = layer.load();
                var configs = {
                    type: "POST",
                    url: url,
                    data: JSON.stringify({ entity:entity })
                };
                uxutil.server.ajax(configs, function (data) {
                    //隐藏遮罩层
                    layer.close(indexs);
                    if (data.success) {
                        layer.msg("新增集合成功,请到具体模块进行新增配置项!", { icon: 6, anim: 0, time: 3000 });
                        mainTable.config.PK = data.value.id;
                        // 重载一下table
                        table.reload('mainTable',{})
//                      layui.event('loadList', 'load', { type: $("#addListModalFormListType").val() });
                        if (index) layer.close(index);
                    } else {
                        layer.msg("新增集合失败！", { icon: 5, anim: 6 });
                    }
                });
            },
            end: function () {
                me.clear();
            }
        });
    };
    //修改集合
    Class.pt.editListClick = function (data) {
        var me = this;
        var Id = data.Id;
        if (!Id) return;
        me.getListInfoByListID(Id, data.typeList, function (list) {
            form.val('addListModalForm', list);
            form.render();
        });
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['修改集合'],
            skin: 'IEQA-class',
            area: ['60%', '65%'],
            content: $('#addListModal'),
            yes: function (index, layero) {
                var entity = {};
                entity["LabID"] = uxutil.cookie.get('LABID') || 0;
//              entity["LabNo"] = $("#addListModalFormLabID").val() ? $("#addListModalFormLabID option:selected").attr("data-labno") : "";
                entity["Id"] = Id;
                entity[$("#addListModalFormListType").val()] = data.Code;
                entity["CName"] = $("#addListModalFormCName").val();
                entity["ShortCode"] = $("#addListModalFormShortCode").val();
                entity["ShortName"] = $("#addListModalFormShortName").val();
                entity["TypeID"] = $("#addListModalFormTypeID").val() ? $("#addListModalFormTypeID").val() : 0;
                entity["TypeName"] = $("#addListModalFormTypeID").val() ? $("#addListModalFormTypeID option:selected").text() : "";
                entity["ClassID"] = $("#addListModalFormClassID").val() ? $("#addListModalFormClassID").val() : 0;
                entity["ClassName"] = $("#addListModalFormClassID").val() ? $("#addListModalFormClassID option:selected").text() : "";
                entity["SourceCodeUrl"] = $("#SourceCodeUrl").val();
                entity["SourceCode"] = $("#SourceCode").val();
                entity["IsUse"] = $("#addListModalFormIsUse").prop("checked") ? 1 : 0;
                entity["DispOrder"] = ($("#addListModalFormDispOrder").val() && !isNaN($("#addListModalFormDispOrder").val())) ? $("#addListModalFormDispOrder").val() : 0;
                var url = "";
                switch ($("#addListModalFormListType").val()) {
                    case "FormCode":
                        url = config.systemHost + me.config.editFormListUrl;
                        break;
                    case "GridCode":
                        url = config.systemHost + me.config.editGridListUrl;
                        break;
                    case "ChartCode":
                        url = config.systemHost + me.config.editChartListUrl;
                        break;
                    default:
                        url = config.systemHost + me.config.editFormListUrl;
                        break;
                }
                var fields = "";
                $.each(entity, function (j, itemJ) {
                    if (!fields)
                        fields = j;
                    else
                        fields += "," + j;
                });
                //显示遮罩层
                var indexs = layer.load();
                var configs = {
                    type: "POST",
                    url: url,
                    data: JSON.stringify({ entity: entity, fields: fields })
                };
                uxutil.server.ajax(configs, function (data) {
                    //隐藏遮罩层
                    layer.close(indexs);
                    if (data.success) {
                        layer.msg("修改集合成功,请查看关联模块内容!", { icon: 6, anim: 0, time: 3000 });
                        // 重载table
                        mainTable.config.PK = Id;
                        table.reload('mainTable',{});
//                      layui.event('loadList', 'load', { type: $("#addListModalFormListType").val() });
                        if (index) layer.close(index);
                    } else {
                        layer.msg("修改集合失败！", { icon: 5, anim: 6 });
                    }
                });
            },
            success: function () {
                $("#addListModalFormListType").addClass("layui-disabled").attr("disabled", "disabled");
                $("#addListModalFormCode").addClass("layui-disabled").attr("disabled", "disabled");
            },
            end: function () {
                $("#addListModalFormListType").removeClass("layui-disabled").attr("disabled", false);
                $("#addListModalFormCode").removeClass("layui-disabled").attr("disabled", false);
                me.clear();
            }
        });
    };
    // 根据集合类型，Id获得集合具体信息(ok)
    Class.pt.getListInfoByListID = function (Id,type,callBack) {
        var me = this,
            Id = Id || null,
            url = '',
            type = type || 'GridCode';
        if (!Id) return;
        if (type === 'ChartCode')
            url = config.systemHost + me.config.getChartListUrl + "?fields=LabID,LabNo,ChartID,ChartCode,TypeID,TypeName,ClassID,ClassName,CName,ShortName,ShortCode,IsUse,DispOrder,SourceCodeUrl,SourceCode,Memo";
        else if (type === 'GridCode')
            url = config.systemHost + me.config.getGridListUrl + "?fields=LabID,LabNo,GridID,GridCode,TypeID,TypeName,ClassID,ClassName,CName,ShortName,ShortCode,IsUse,DispOrder,SourceCodeUrl,SourceCode,Memo";
        else if (type === 'FormCode')
            url = config.systemHost + me.config.getFormListUrl + "?fields=LabID,LabNo,FormID,FormCode,TypeID,TypeName,ClassID,ClassName,CName,ShortName,ShortCode,IsUse,DispOrder,SourceCodeUrl,SourceCode,Memo";
        var loadIndex = layer.load();
        uxutil.server.ajax({
            url: url + "&Id=" + Id
        }, function (res) {
                layer.close(loadIndex);
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = {
                        addListModalFormLabID: res["value"]["LabID"],
                        addListModalFormListType: type,
                        addListModalFormCode: res["value"][type],
                        addListModalFormCName: res["value"]["CName"],
                        addListModalFormShortCode: res["value"]["ShortCode"],
                        addListModalFormShortName: res["value"]["ShortName"],
                        addListModalFormTypeID: res["value"]["TypeID"],
                        addListModalFormClassID: res["value"]["ClassID"],
                        addListModalFormDispOrder: res["value"]["DispOrder"],
                        addListModalFormIsUse: res["value"]["IsUse"],
                        SourceCodeUrl: res["value"]["SourceCodeUrl"],
                        SourceCode: res["value"]["SourceCode"]
                    };
                    if (typeof (callBack) == 'function') callBack(list);
                }
            } else {
                layer.msg("获得集合失败！");
            }
        });
    };
    //新增---nouse
    Class.pt.addClick = function () {
        var me = this;
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['新增配置项'],
            skin: 'IEQA-class',
            area: ['90%', '90%'],
            content: $('#addModal'),
            yes: function (index, layero) {
                var entity = {};
                entity["ModuleID"] = config.ModuleID;
                entity["ModuleFormGridLinkID"] = $("#Id").val() ? $("#Id").val() : 0;
                entity["LabID"] = $("#LabID").val() ? $("#LabID").val() : 0;
                entity["LabNo"] = $("#LabID").val() ? $("#LabID option:selected").attr("data-labno") : "";
                entity["FormID"] = $("#FormID").val() ? $("#FormID").val() : null;
                entity["FormCode"] = $("#FormID").val() ? $("#FormID option:selected").attr("data-code") : "";
                entity["GridID"] = $("#GridID").val() ? $("#GridID").val() : null;
                entity["GridCode"] = $("#GridID").val() ? $("#GridID option:selected").attr("data-code") : "";
                entity["ChartID"] = $("#ChartID").val() ? $("#ChartID").val() : null;
                entity["ChartCode"] = $("#ChartID").val() ? $("#ChartID option:selected").attr("data-code") : "";
                if ($("#FormID").val()) {
                    entity["CName"] = $("#FormID option:selected").text();
                    entity["ShortCode"] = $("#FormID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#FormID option:selected").attr("data-shortname");
                } else if ($("#GridID").val()) {
                    entity["CName"] = $("#GridID option:selected").text();
                    entity["ShortCode"] = $("#GridID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#GridID option:selected").attr("data-shortname");
                } else if ($("#ChartID").val()) {
                    entity["CName"] = $("#ChartID option:selected").text();
                    entity["ShortCode"] = $("#ChartID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#ChartID option:selected").attr("data-shortname");
                } else {
                    entity["CName"] = "";
                    entity["ShortCode"] = "";
                    entity["ShortName"] = "";
                }
                entity["IsUse"] = $("#IsUse").prop("checked") ? 1 : 0;
                entity["DispOrder"] = ($("#DispOrder").val() && !isNaN($("#DispOrder").val())) ? $("#DispOrder").val() : 0;
                me.onSaveClick({ entity: entity }, 'add', index);
            },
            end: function () {
                me.clear();
            }
        });
    };
    //修改---nouse
    Class.pt.editClick = function (data) {
        var me = this;
        var list = JSON.parse(JSON.stringify(data));
        list.BModuleModuleFormGridLink_IsUse = String(list.BModuleModuleFormGridLink_IsUse) == "true" ? true : false;
		list.Id = list.BModuleModuleFormGridLink_Id;
        form.val('addModalForm', list);
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['修改勾选项'],
            skin: 'IEQA-class',
            area: ['90%', '90%'],
            content: $('#addModal'),
            yes: function (index, layero) {
                var entity = {};
                entity["ModuleID"] = config.ModuleID;
                //entity["ModuleFormGridLinkID"] = $("#Id").val() ? $("#Id").val() : 0;
                entity["Id"] = $("#Id").val() ? $("#Id").val() : 0;
                entity["LabID"] = $("#LabID").val() ? $("#LabID").val() : 0;
                entity["LabNo"] = $("#LabID").val() ? $("#LabID option:selected").attr("data-labno") : "";
                entity["FormID"] = $("#FormID").val() ? $("#FormID").val() : null;
                entity["FormCode"] = $("#FormID").val() ? $("#FormID option:selected").attr("data-code") : "";
                entity["GridID"] = $("#GridID").val() ? $("#GridID").val() : null;
                entity["GridCode"] = $("#GridID").val() ? $("#GridID option:selected").attr("data-code") : "";
                entity["ChartID"] = $("#ChartID").val() ? $("#ChartID").val() : null;
                entity["ChartCode"] = $("#ChartID").val() ? $("#ChartID option:selected").attr("data-code") : "";
                if ($("#FormID").val()) {
                    entity["CName"] = $("#FormID option:selected").text();
//                  entity["ShortCode"] = $("#FormID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#FormID option:selected").attr("data-shortname");
                } else if ($("#GridID").val()) {
                    entity["CName"] = $("#GridID option:selected").text();
//                  entity["ShortCode"] = $("#GridID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#GridID option:selected").attr("data-shortname");
                } else if ($("#ChartID").val()) {
                    entity["CName"] = $("#ChartID option:selected").text();
//                  entity["ShortCode"] = $("#ChartID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#ChartID option:selected").attr("data-shortname");
                } else {
                    entity["CName"] = "";
//                  entity["ShortCode"] = "";
                    entity["ShortName"] = "";
                }
                entity["IsUse"] = $("#IsUse").prop("checked") ? 1 : 0;
                entity["DispOrder"] = ($("#DispOrder").val() && !isNaN($("#DispOrder").val())) ? $("#DispOrder").val() : 0;
                var fields = "";
                $.each(entity, function (j, itemJ) {
                    if (!fields)
                        fields = j;
                    else
                        fields += "," + j;
                });
                me.onSaveClick({ entity: entity, fields: fields }, 'edit', index);
            },
            end: function () {
                me.clear();
            }
        });
    }
    //删除---nouse
    Class.pt.delClick = function (checkData) {
        var me = this;
        var len = checkData.length,
            successCount = 0,
            failCount = 0;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var indexs = layer.load();//显示遮罩层
            $.each(checkData, function (i, item) {
                var url = me.config.delUrl + '?id=' + item.BModuleModuleFormGridLink_Id;
                uxutil.server.ajax({
                    url: url
                }, function (data) {
                    if (data.success === true) {
                        successCount++;
                    } else {
                        failCount++;
                    }
                    if (successCount + failCount == len) {
                        table.reload(me.config.id, {});
                        layer.close(indexs);
                        layer.close(index);
                        layer.msg("删除完毕！成功：" + successCount + "个，失败：" + failCount + "个", { time: 3000 });
                    }
                });
            });
        });
    };
    //表单保存处理--nouse
    Class.pt.onSaveClick = function (data, type, index) {
        var me = this,
            url = type == 'add' ? me.config.addUrl : me.config.editUrl,
            params = data;
        if (!url) return;
        if (!params.entity.ModuleID) return;
        if (type == "add") delete params.entity.ModuleFormGridLinkID;
        if ((params.entity.FormID && params.entity.GridID) || (params.entity.FormID && params.entity.ChartID) || (params.entity.ChartID && params.entity.GridID)) {
            layer.msg("表单集合、表格集合、图表集合不能同时存在!");
            return;
        }
        if (!params.entity.FormID && !params.entity.GridID && !params.entity.ChartID) {
            layer.msg("必须存在一种配置项!");
            return;
        }
		if (!params.entity.FormID) {
		   delete params.entity.FormID;
		}
		if (!params.entity.GridID) {
		   delete params.entity.GridID;
		}
		if (!params.entity.ChartID) {
		   delete params.entity.ChartID;
		}
        params = JSON.stringify(params);
        //显示遮罩层
        var indexs = layer.load();
        var configs = {
            type: "POST",
            url: url,
            data: params
        };
        uxutil.server.ajax(configs, function (data) {
            //隐藏遮罩层
            layer.close(indexs);
            if (data.success) {
                if (index) layer.close(index);
                layer.msg("保存成功！", { icon: 6, anim: 0 });
                table.reload(me.config.id, {});
            } else {
                var msg = type == 'add' ? '新增失败！' : '修改失败！';
                if (!data.msg) data.msg = msg;
                layer.msg(data.msg, { icon: 5, anim: 6 });
            }
        });
    };
    //清空表单
    Class.pt.clear = function () {
        var me = this;
        $("#addModalForm :input").each(function (i, item) {
            $(this).val("");
        });
        $("#addListModalForm :input").each(function (i, item) {
            if ($(this).attr("id") == "addListModalFormListType")
                $(this).val("FormCode");
            else
                $(this).val("");
        });
        form.render();
    };
    //设置配置项
    Class.pt.setConfigItem = function (data) {
        var me = this,
            url = "",
            options = {};
        if (me.config.role == 'system') {
            if (data["AggregateType"] === 'form') {
                url = uxutil.path.ROOT + '/ui/layui/views/system/setting/formItem/main/app.html?isSaveSuccessLoadTable=true&isSearch=false';
                options = { FormID: data.Id, FormCode: data.Code, CName: data.Name,systemHost:config.systemHost };
            } else if (data["AggregateType"] === 'grid') {
                url = uxutil.path.ROOT + '/ui/layui/views/system/setting/tableColumn/main/app.html?isSaveSuccessLoadTable=true&isSearch=false';
                options = { GridID: data.Id, GridCode: data.Code, CName: data.Name,systemHost:config.systemHost  };
            } else if (data["AggregateType"] === 'chart') {
                layer.msg("暂不支持图表设置配置项!");
                return;
            }
        } else {
            if (data["AggregateType"] === 'form') {
                url = uxutil.path.ROOT + '/ui/layui/views/system/setting/formItem_client/main/app.html?isSearch=false';
                options = { FormCode: data.Code,systemHost:config.systemHost };
            } else if (data["AggregateType"] === 'grid') {
                url = uxutil.path.ROOT + '/ui/layui/views/system/setting/tableColumn_client/main/app.html?isSearch=false';
                options = { GridCode: data.Code,systemHost:config.systemHost };
            } else if (data["AggregateType"] === 'chart') {
                layer.msg("暂不支持图表设置配置项!");
                return;
            }
        }
        layer.open({
            type: 2,
            title: ['设置配置项'],
            skin: 'IEQA-class',
            area: ['90%', '90%'],
            content: url,
            success: function (layero, index) {
                var indexs = layer.load();
                var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                $(body).css("padding", "0 5px");
                setTimeout(function () {
                    var iframeWin = window[layero.find('iframe')[0]['name']];
                    iframeWin.layui.mainTable.onSearch('mainTable', options);
                    layer.close(indexs);
                },500);
            }
        });
    };
      /***
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
    var doAutoSelect = function (curTable, rowIndex) {
        curTable.key = curTable.id;
        var data = table.cache[curTable.key] || [];
        if (!data || data.length <= 0) return;
        rowIndex = rowIndex || 0;
        var tableDiv = $(curTable.elem + "+div .layui-table-body.layui-table-body.layui-table-main");
        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
        var filter = $(curTable.elem).find('lay-filter');
        var obj = {
            tr: thatrow,
            data: data[rowIndex] || {},
            del: function () {
                table.cache[curTable.key][index] = [];
                tr.remove();
                curTable.scrollPatch();
            },
            updte: {}
        };
        layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
        thatrow.click();
    };
    //暴露接口
    exports('mainTable', mainTable);
});
