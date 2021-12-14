/**
	@name：页面配置表单项-客户
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

    var config = { FormCode: '', BModuleFormControlListIds: [], loadIndex: null,
    	systemHost: ''
    };

    var mainTable = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
        config: {
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
            sort: [{
                "property": 'BModuleFormControlSet_DispOrder',
                "direction": 'ASC'
            }],
            selectUrl:  '/ServerWCF/ModuleConfigService.svc/ST_UDTO_SearchBModuleFormControlSetByHQL?isPlanish=true',
            addListUrl: '/ServerWCF/ModuleConfigService.svc/AddBModuleFormControlSets',
            editListUrl:  '/ServerWCF/ModuleConfigService.svc/EditBModuleFormControlSets',
            //addUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_AddBModuleFormControlSet',
            //editUrl:  '/ServerWCF/ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormControlSetByField',
            delUrl:  '/ServerWCF/ModuleConfigService.svc/ST_UDTO_DelBModuleFormControlSet',
            where: "",
            toolbar: '#Toolbar',
            defaultToolbar: ['filter'],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 300, 500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'checkbox' },
                { type: 'numbers', title: '行号' },
                { field: 'BModuleFormControlSet_Id', width: 80, title: '主键ID', sort: false, hide: true },
                { field: 'BModuleFormControlSet_FormControlID', width: 80, title: 'BModuleFormControlList表主键ID', sort: false, hide: true },
                { field: 'BModuleFormControlSet_LabID', width: 80, title: '实验室ID', sort: false, hide: true },
                { field: 'BModuleFormControlSet_LabNo', title: '实验室No', width: 120, sort: true, hide: true },
                { field: 'BModuleFormControlSet_FormCode', title: '表单代码', width: 120, sort: true, hide: true },
                { field: 'BModuleFormControlSet_CName', title: '名称', width: 160, sort: true, hide: true },
                { field: 'BModuleFormControlSet_Label', title: '显示名称', width: 120, sort: true, edit: 'text' },
                { field: 'BModuleFormControlSet_DefaultValue', title: '默认值', width: 120, sort: true, edit: 'text' },
                { field: 'BModuleFormControlSet_DispOrder', title: '显示次序', width: 120, sort: true,templet: '#DispOrderTpl'},
                { field: 'BModuleFormControlSet_IsReadOnly', title: '是否只读', width: 120, sort: true, templet: '#IsReadOnlyTpl' },
                { field: 'BModuleFormControlSet_IsDisplay', title: '是否显示', width: 120, sort: true, templet: '#IsDisplayTpl' },
                { field: 'BModuleFormControlSet_IsUse', title: '是否使用', width: 120, sort: true, templet: '#IsUseTpl' },
                { field: 'BModuleFormControlSet_Tab', width: 100, title: '用于判断行是否有修改数据', hide: true, sort: false }
            ]],
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                var list = [];
                config.BModuleFormControlListIds = [];
                if (data && data.list) list = data.list;
                $.each(list, function (i, item) {
                    config.BModuleFormControlListIds.push(item.BModuleFormControlSet_FormControlID);
                });
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
            	var me = this;
                if (config.loadIndex) layer.close(config.loadIndex);
                //无数据处理
                if (count == 0) return;
                //设置表格中复选框样式
                $("input[name='DispOrder']").parent('div.layui-table-cell').css({ 'overflow': 'visible', 'padding': '0px' });
                $("input[name='DispOrder']").parent('div.layui-table-cell').parent('td').css({ 'overflow': 'visible', 'padding': '0px' });
                var height = $("input[name='DispOrder']").parent('div.layui-table-cell').parent('td').height();
                $("input[name='DispOrder']").css({ 'height': height + 'px','margin-top':'-11px','border-color': '#fff'});
                // 显示次序的监听
                $("input[name='DispOrder']").on('change',function(e){
				    var inputValue = this.value; // 单元格里面最新的值
		        	var td = $(this).parent().parent('td');
		        	var tr = td.parent('tr');
		        	var dataindex = tr.attr("data-index");
			        var tableCache = table.cache[me.id];
			        $.each(tableCache, function (index, value) {
		                if (index == dataindex) {
		                    value.BModuleFormControlSet_Tab = true;
			                value.BModuleFormControlSet_DispOrder = inputValue;
		                }
		            });
		        });
                //设置表格中复选框样式
                $("select[name='IsUse']").parent('div.layui-table-cell').css({ 'overflow': 'visible', 'padding': '0 15px' });
                //触发点击事件
                $("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();

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
        var me = this, arr = [];
        var url = config.systemHost + me.config.selectUrl;
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
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
        if (config.FormCode) arr.push("FormCode='" + config.FormCode + "'");
		//arr.push("QFuncID is null");
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + where;
        }
        var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        if (defaultOrderBy && defaultOrderBy.length > 0) url += '&sort=' + JSON.stringify(defaultOrderBy);

        return url;
    };
    //获取查询Fields
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
        inst.tableIns = table.render(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    mainTable.onSearch = function (mytable, options) {
        var me = this;
        if (options) config = $.extend({}, config, options);
        var inst = new Class(me);
        mainTable.url = inst.getLoadUrl();
        mainTable.config.elem = "#" + mytable;
        mainTable.config.id = mytable;
        config.loadIndex = layer.load();
        table.reload(mytable, {
            url: inst.getLoadUrl()
        });
    };
    Class.pt.iniListeners = function () {
        var me = this;
        //监听表格头工具栏
        table.on('toolbar(mainTable)', function (obj) {
            var layEvent = obj.event;
            if (layEvent == 'add') {//新增
                if (config.FormCode) me.addClick();
            } else if (layEvent === 'del') { //删除
                me.delClick();
            } else if (layEvent === 'edit') { //编辑
                me.editClick();
            }
        });
        // 行工具栏的监听
        /*table.on('tool(mainTable)', function(obj){
        	var data = obj.data; //获得当前行数据
		    var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
		    var tr = obj.tr; //获得当前行 tr 的 DOM 对象（如果有的话）
		    // var inputValue = tr.find('input[type="number"]').val(); // 单元格里面的值
		    var inputValue = this.value; // 单元格里面的值
		    if(layEvent === 'DispOrder') {
		    	// 监听用户输入数值后，重新赋值table里面的数据
		    	this.onblur = function(e){
		    		var inputValue2 = e.target.value;
		    		if(data.BModuleFormControlSet_DispOrder != inputValue2) {
			    		var dataindex = tr.attr("data-index");
			            var tableCache = table.cache[me.config.id];
			            //改变后的数据
			            var rowObj = tableCache[dataindex].BModuleFormControlSet_Tab;
			            if (rowObj) delete rowObj.BModuleFormControlSet_IsUse;
			            if (!rowObj) rowObj = {};
			            $.each(tableCache, function (index, value) {
			                if (index == dataindex) {
			                    value.BModuleFormControlSet_Tab = true;
			                    value.BModuleFormControlSet_DispOrder = inputValue2;
			                }
			            });
			    	}
		    	}
		    	// 监听用户点击number这个框时，重新赋值table里面的数据
		    	if(data.BModuleFormControlSet_DispOrder != inputValue) {
		    		var dataindex = tr.attr("data-index");
		            var tableCache = table.cache[me.config.id];
		            //改变后的数据
		            var rowObj = tableCache[dataindex].BModuleFormControlSet_Tab;
		            if (rowObj) delete rowObj.BModuleFormControlSet_IsUse;
		            if (!rowObj) rowObj = {};
		            $.each(tableCache, function (index, value) {
		                if (index == dataindex) {
		                    value.BModuleFormControlSet_Tab = true;
		                    value.BModuleFormControlSet_DispOrder = inputValue;
		                }
		            });
		    	}
		    	
		    }
        });*/
        //是否使用
        form.on('checkbox(IsReadOnly)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr');
            var dataindex = elem.attr("data-index");
            var tableCache = table.cache[me.config.id];
            //改变后的数据
            var rowObj = tableCache[dataindex].BModuleFormControlSet_Tab;
            if (rowObj) delete rowObj.BModuleFormControlSet_IsReadOnly;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (index == dataindex) {
                    value.BModuleFormControlSet_Tab = true;
                    value.BModuleFormControlSet_IsReadOnly = data.elem.checked;
                }
            });
        });
        //是否使用
        form.on('checkbox(IsDisplay)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr');
            var dataindex = elem.attr("data-index");
            var tableCache = table.cache[me.config.id];
            //改变后的数据
            var rowObj = tableCache[dataindex].BModuleFormControlSet_Tab;
            if (rowObj) delete rowObj.BModuleFormControlSet_IsDisplay;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (index == dataindex) {
                    value.BModuleFormControlSet_Tab = true;
                    value.BModuleFormControlSet_IsDisplay = data.elem.checked;
                }
            });
        });
        //是否使用
        form.on('checkbox(IsUse)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr');
            var dataindex = elem.attr("data-index");
            var tableCache = table.cache[me.config.id];
            //改变后的数据
            var rowObj = tableCache[dataindex].BModuleFormControlSet_Tab;
            if (rowObj) delete rowObj.BModuleFormControlSet_IsUse;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (index == dataindex) {
                    value.BModuleFormControlSet_Tab = true;
                    value.BModuleFormControlSet_IsUse = data.elem.checked;
                }
            });
        });
        //监听单元格编辑
        table.on('edit(' + me.config.id + ')', function (obj) {
            var value = obj.value, //得到修改后的值
                data = obj.data,//得到所在行所有键值
                field = obj.field; //得到字段
            var tableCache = table.cache[me.config.id];
            var dataindex = obj.tr[0].dataset.index;
            tableCache[dataindex].BModuleFormControlSet_Tab = true;
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
    };
    //新增
    Class.pt.addClick = function () {
        var me = this,
            where = "IsUse <> 0 and FormCode in ('" + config.FormCode + "')";
        if (config.BModuleFormControlListIds.length > 0) where += " and FormControlID not in (" + config.BModuleFormControlListIds.join(",") + ")";
        layer.open({
            type: 2,
            btn: ['追加选中项'],
            title: ['追加配置项'],
            skin: 'IEQA-class',
            area: ['90%', '90%'],
            content: uxutil.path.ROOT + '/ui/layui/views/system/setting/formItem/main/app.html?toolbar=false&defaultToolbar=&externalWhere=' + where + '&isHideColumnToolbar=true&isInitLaboratory=false&isInitComponentType=false&isSearch=true',
            yes: function (index, layero) {
                var iframeWin = window[layero.find('iframe')[0]['name']],
                    checkData = iframeWin.layui.table.checkStatus('mainTable').data,
                    len = checkData.length;
                if (len == 0) {
                    layer.msg("请选择需要追加的配置项！");
                    return;
                }
                var loadIndex = layer.load();
                // 改为批量新增
                var obj = {
                	BModuleFormControlSets: []
                };
                $.each(checkData, function (i, item) {
                    var params = {
                        FormControlID: item.BModuleFormControlList_Id,
                        LabID: uxutil.cookie.get(uxutil.cookie.map.LABID),
                        LabNo: "",
                        FormCode: item.BModuleFormControlList_FormCode,
                        CName: item.BModuleFormControlList_CName,
                        DefaultValue: item.BModuleFormControlList_DefaultValue,
                        Label: item.BModuleFormControlList_Label,
                        DispOrder: item.BModuleFormControlList_DispOrder,
                        IsReadOnly: item.BModuleFormControlList_IsReadOnly,
                        IsDisplay: item.BModuleFormControlList_IsDisplay,
                        IsUse: item.BModuleFormControlList_IsUse

                    };
                    obj.BModuleFormControlSets.push(params);
                    
                });
                var configs = {
                    type: "POST",
                    url: config.systemHost + me.config.addListUrl,
                    data: JSON.stringify(obj)
                };
                uxutil.server.ajax(configs, function (data) {
                    if (data.success) {
                       table.reload(me.config.id, {});
                       layer.close(index);
                       layer.close(loadIndex);
                    } 
                });
            },
            success: function (layero, index) {
                var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                $(body).css("padding", "0 5px");

            }
        });
    };
    //编辑
    Class.pt.editClick = function () {
        var me = this,
            tableCache = table.cache[me.config.id],
            arr = [];
        if (!tableCache || tableCache.length == 0) {
            layer.msg("没有数据不需要保存！");
            return;
        }
        $.each(tableCache, function (i, item) {
            if (item.BModuleFormControlSet_Tab) arr.push(item);
        });
        var len = arr.length;
        if (len == 0) {
            layer.msg("没有修改数据不需要保存！");
            return;
        }
        var loadIndex = layer.load();
        var list = [];
        $.each(arr, function (i, item) {
            var params = {
                BModuleFormControlSet: {
                    Id: item.BModuleFormControlSet_Id,
                    DefaultValue: item.BModuleFormControlSet_DefaultValue,
                    Label: item.BModuleFormControlSet_Label,
                    DispOrder: item.BModuleFormControlSet_DispOrder,
                    IsUse: item.BModuleFormControlSet_IsUse,
                    IsReadOnly: item.BModuleFormControlSet_IsReadOnly,
                    IsDisplay: item.BModuleFormControlSet_IsDisplay
                },
                fields: 'Id,DefaultValue,Label,DispOrder,IsReadOnly,IsDisplay,IsUse'
            };
            list.push(params)
        });
        var configs = {
            type: "POST",
            url: config.systemHost + me.config.editListUrl,
            data: JSON.stringify({BModuleFormControlSetVOs : list})
        };
        uxutil.server.ajax(configs, function (data) {
            if (data.success) {
				table.reload(me.config.id, {});
                layer.close(loadIndex);
            } 
        });
    };
    //删除
    Class.pt.delClick = function () {
        var me = this,
            checkData = table.checkStatus(me.config.id).data;
        if (checkData && checkData.length == 0) {
            layer.msg("请选择需要删除的数据！");
            return;
        }
        var len = checkData.length,
            successCount = 0,
            failCount = 0;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var indexs = layer.load();//显示遮罩层
            $.each(checkData, function (i, item) {
                var url = config.systemHost + me.config.delUrl + '?id=' + item.BModuleFormControlSet_Id;
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
    //暴露接口
    exports('mainTable', mainTable);
});
