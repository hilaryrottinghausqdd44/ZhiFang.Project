/**
	@name：仪器项目列表
	@author：zhangda	
	@version 2019-08-19
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form,
        uxtable = layui.uxtable;
    var config = { equipID: null, equipCName: '', sectionID: null, IsBtnClick: true };
    var itemTable = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
        config: {
            elem: '',
            id: "",
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
                "property": 'LBItem_DispOrder',
                "direction": 'ASC'
            }],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipItemVOByHQL?isPlanish=true',
            delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBEquipItem',
            editUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipItemByField',
            //查项目，
            selectItemUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true&sort=[{property:"LBItem_DispOrder",direction:"ASC"}]',
            //查小组，
            selectSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true&sort=[{property:"LBSection_DispOrder",direction:"ASC"}]',
            //获得组合项目
            //getGroupItemUrl: ,
            where: "",
            toolbar: '',
            page: false,
            limit: 500,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号' },
                { field: 'LBEquipItemVO_LBEquipItem_Id', width: 150, title: '主键', sort: true, hide: true },
                { field: 'LBEquipItemVO_LBItem_Id', width: 150, title: '项目编号', sort: true, hide: true },
                { field: 'LBEquipItemVO_LBItem_CName', minWidth: 150, flex: 1, title: '项目名称', sort: true },
                { field: 'LBEquipItemVO_LBItem_SName', width: 100, title: '项目简称', sort: true },
                { field: 'LBEquipItemVO_LBEquipItem_CompCode', width: 150, title: '仪器对照通道标识', sort: true, edit: 'text' },
                { field: 'LBEquipItemVO_LBEquipItem_IsReserve', width: 100, title: '备用通道', sort: true, align:'center', templet: '#IsReserveTpl' },
                { field: 'LBEquipItemVO_LBEquipItem_PItemID', width: 150, title: '缺省组合项目', sort: true, templet:'#groupItemTpl' },
                { field: 'LBEquipItemVO_LBEquipItem_SectionID', width: 150, title: '特定小组', sort: true, templet: '#SectionIDTpl' },
                { field: 'LBEquipItemVO_LBEquipItem_DilutionMultiple', width: 150, title: '双向稀释倍数', sort: true },
                { field: 'LBEquipItemVO_LBEquipItem_DispOrderComm', width: 150, title: '通讯发送顺序', sort: true },
                { field: 'LBEquipItemVO_LBEquipItem_DispOrderQC', width: 150, title: '质控显示次序', sort: true },
                { field: 'LBEquipItemVO_Tab', width: 150, title: '用于判断行是否有修改数据', hide: true, sort: true }
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n").replace(/\u0009/g,"")) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {

                $("select[name='groupitem']").parent('div.layui-table-cell').css('overflow', 'visible');
                $("select[name='section']").parent('div.layui-table-cell').css('overflow', 'visible');

                //$("select[name='groupitem']").empty();
                //$("select[name='groupitem']").append(comboxData.groupitemData);

                $("select[name='section']").empty();
                $("select[name='section']").append(comboxData.sectionData);

                var that = this.elem.next();
                for (var i = 0; i < res.data.length; i++) {
                    var PItemID = res.data[i].LBEquipItemVO_LBEquipItem_PItemID;
                    var SectionID = res.data[i].LBEquipItemVO_LBEquipItem_SectionID;
                    var trRow = that.find(".layui-table-box tbody tr[data-index='" + i + "']");
                    var ItemId = null;
                    $(trRow).find("td").each(function () {
                        var fieldName = $(this).attr("data-field");
                        var selectJq = $(this).find("select");
                        //赋值组合项目 选中值
                        if (fieldName == 'LBEquipItemVO_LBEquipItem_PItemID') {
                            ItemId = res.data[i].LBEquipItemVO_LBItem_Id;
                            Class.pt.loadGroupItemDataByItemId(ItemId, trRow, $(this), PItemID, function (selectData,tr,td,Id) {
                                tr.find("select[name='groupitem']").html(selectData);
                                td.children().children().val(Id);
                                form.render('select');
                            });
                        }
                        //选中值
                        if (selectJq.length == 1) {
                            //if (PItemID == res.data[i][fieldName]) {
                            //    $(this).children().children().val(PItemID);
                            //}
                            if (SectionID == res.data[i][fieldName]) {
                                $(this).children().children().val(SectionID);
                            }
                        }
                    });
                }
                form.render('select');
            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //列表内下拉框数据
    var comboxData = function () {
        //组合项目下拉框内容
        groupitemData: []
        //小组下拉框内容
        sectionData: []
    };
    //构造器
    var Class = function (options) {
        var me = this;
        me.config = $.extend({}, itemTable.config, me.config, options);
        if (me.config.defaultLoad) me.config.url = me.getLoadUrl();
        me = $.extend(true, {}, itemTable, Class.pt, me);// table,
        return me;
    };
    Class.pt = Class.prototype;
    //获取查询Url
    Class.pt.getLoadUrl = function () {
        var me = this, arr = [];
        var url = me.config.selectUrl;
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
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + JShell.String.encode(where);
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
    itemTable.render = function (options) {
        var me = this;
        var inst = new Class(options);
        inst.tableIns = table.render(inst.config);
        //inst.loadItemData();
        inst.loadSectionData();
        //监听
        inst.iniListeners();
        //		inst.updateCacheForm("listTableJtcy", "listTableIdJtcy", "form");
        return inst;
    };
    //对外公开-数据加载
    itemTable.onSearch = function (mytable, checkRow) {
        var me = this;
        var EquipID = checkRow.LBEquip_Id;
        config.equipID = EquipID;
        config.sectionID = checkRow.LBEquip_LBSection_Id;
        config.equipCName = checkRow.LBEquip_CName;
        var inst = new Class(me);
        var where = 'lbequip.Id=' + EquipID;
        itemTable.where = where;
        itemTable.url = inst.getLoadUrl();
        itemTable.elem = "#" + mytable;
        itemTable.id = mytable;
        //      inst.config.where = where; 
        table.reload(mytable, {
            url: inst.getLoadUrl(),
            where: {
                where: where
            }
        });
    };
    //对外公开-刷新列表
    itemTable.onRefresh = function () {
        var me = this;
        table.reload(itemTable.id, {
            where: {
                t: new Date().getTime()
            }
        });
    };
    //组合项目下拉框-下拉框数据读取
    Class.pt.loadItemData = function () {
        var me = this;
        var url = me.config.selectItemUrl + '&fields=LBItem_CName,LBItem_Id';
        url += '&where=(lbitem.IsUse=1 and lbitem.GroupType=1)';
        //组合项目下拉框内容
        comboxData.groupitemData = [];
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
                if (!value) return;
                var tempAjax = "<option value=''>请选择</option>";

                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBItem_Id + "'>" + value.list[i].LBItem_CName + "</option>";
                }
                comboxData.groupitemData = tempAjax;
            } else {
                layer.msg(data.msg);
            }
        });
    };
    //小组下拉框-下拉框数据读取
    Class.pt.loadSectionData = function () {
        var me = this;
        var url = me.config.selectSectionUrl + '&where=IsUse=1' +
            '&fields=LBSection_CName,LBSection_Id';
        //组合项目下拉框内容
        comboxData.sectionData = [];
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
                if (!value) return;
                var tempAjax = "<option value=''>请选择</option>";

                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBSection_Id + "'>" + value.list[i].LBSection_CName + "</option>";
                }
                comboxData.sectionData = tempAjax;

            } else {
                layer.msg(data.msg);
            }
        });
    };
    //根据ItemId获得所属组合项目
    Class.pt.loadGroupItemDataByItemId = function (ItemId, trRow, td,PItemID,callBack) {
        var me = this;
        var url = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true&sort=[{property:"LBItemGroup_LBItem_DispOrder",direction:"ASC"}]';
        url += '&fields=LBItemGroup_LBGroup_Id,LBItemGroup_LBGroup_CName';
        url += '&where=lbitem.Id=' + ItemId +' and lbgroup.GroupType=1 and lbgroup.IsUse=1';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
                if (!value) return;
                var tempAjax = "<option value=''>请选择</option>";

                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBItemGroup_LBGroup_Id + "'>" + value.list[i].LBItemGroup_LBGroup_CName + "</option>";
                }
                if (typeof (callBack) == "function") {
                    callBack(tempAjax, trRow, td, PItemID);
                }
            } else {
                layer.msg(data.msg);
            }
        });
    }
    //监听方法
    Class.pt.iniListeners = function () {
        var me = this;
        //默认组合项目选择
        form.on('select(groupitem)', function (data) {
            //这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
            var elem = data.othis.parents('tr');
            var dataindex = elem.attr("data-index");
            var tableCache = table.cache[me.config.id];

            //改变后的数据
            var rowObj = tableCache[dataindex].LBEquipItemVO_Tab;
            if (rowObj) delete rowObj.LBEquipItemVO_LBEquipItem_PItemID;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (value.LAY_TABLE_INDEX == dataindex) {
                    if (data.value) rowObj.LBEquipItemVO_LBEquipItem_PItemID = data.value;
                    value.LBEquipItemVO_Tab = rowObj;
                    value.LBEquipItemVO_LBEquipItem_PItemID = data.value;
                }
            });
        });
        //仪器
        form.on('select(section)', function (data) {
            //这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
            var elem = data.othis.parents('tr');
            var dataindex = elem.attr("data-index");
            var tableCache = table.cache[me.config.id];
            //改变后的数据
            var rowObj = tableCache[dataindex].LBEquipItemVO_Tab;
            if (rowObj) delete rowObj.LBEquipItemVO_LBEquipItem_SectionID;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (value.LAY_TABLE_INDEX == dataindex) {
                    if (data.value) rowObj.LBEquipItemVO_LBEquipItem_SectionID = data.value;
                    value.LBEquipItemVO_Tab = rowObj;
                    value.LBEquipItemVO_LBEquipItem_SectionID = data.value;
                }
            });
        });
        form.on('checkbox(IsReserve)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr');
            var dataindex = elem.attr("data-index");
            var tableCache = table.cache[me.config.id];
            //改变后的数据
            var rowObj = tableCache[dataindex].LBEquipItemVO_Tab;
            if (rowObj) delete rowObj.LBEquipItemVO_LBEquipItem_IsReserve;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (value.LAY_TABLE_INDEX == dataindex) {
                    if (data.value) rowObj.LBEquipItemVO_LBEquipItem_IsReserve = data.elem.checked;
                    value.LBEquipItemVO_Tab = rowObj;
                    value.LBEquipItemVO_LBEquipItem_IsReserve = data.elem.checked;
                }
            });
        });

        //监听单元格编辑
        table.on('edit(' + me.config.id + ')', function (obj) {
            var value = obj.value, //得到修改后的值
                data = obj.data,//得到所在行所有键值
                field = obj.field; //得到字段
            var tableCache = table.cache[me.config.id];
            var dataindex = 0;
            for (var i = 0; i < tableCache.length; i++) {
                if (tableCache[i].LBEquipItemVO_LBEquipItem_Id == data.LBEquipItemVO_LBEquipItem_Id) {
                    dataindex = i;
                    break;
                }

            }
            //改变后的数据
            var rowObj = tableCache[dataindex].LBEquipItemVO_Tab;
            if (rowObj) delete rowObj.LBEquipItemVO_LBEquipItem_CompCode;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (value.LAY_TABLE_INDEX == dataindex) {
                    if (data.value) rowObj.LBEquipItemVO_LBEquipItem_CompCode = value;
                    value.LBEquipItemVO_Tab = rowObj;
                }
            });
        });
        //保存修改行
        $('#saveEquipItem').on('click', function () {
            if (!config.IsBtnClick) return;
            config.IsBtnClick = false;
            try {
                me.onSaveClick();
            } catch (err) {
                layer.msg(err);
            } finally {
                config.IsBtnClick = true;
            }
        });
        //选择仪器项目
        $('#addGroupItem').on('click', function () {
            if (!config.sectionID) {
                layer.msg('请先为仪器设置小组!', { icon: 0, anim:0 });
                return;
            }
            var flag = false;
            parent.layer.open({
                type: 2,
                area: me.screen($) < 2 ? ['85%', '70%'] : ['1200px', '600px'],
                fixed: false,
                maxmin: false,
                title: '选择仪器项目',
                content: uxutil.path.ROOT + '/ui/layui/app/dic/equip/item/check_item/app.html?sectionID=' + config.sectionID + '&equipID=' + config.equipID + '&equipCName=' + config.equipCName,
                cancel: function (index, layero) {
                    flag = true;
                },
                success: function (layero, index) {
                    var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                    body.find('#sectionID').val(config.sectionID);
                    body.find('#equipID').val(config.equipID);
                    body.find('#equipCName').html(config.equipCName);
                },
                end: function () {
                    if (flag) return;
                    table.reload(itemTable.id);
                }
            });
        });
        //仪器项目质控排序
        $('#equipItemQCSort').on('click', function () {
            var flag = false;
            parent.layer.open({
                type: 2,
                title: ['仪器项目质控排序调整'],
                area: ['1200px', '620px'],
                content: uxutil.path.ROOT + '/ui/layui/app/dic/equip/item/QC_sort/app.html?' + config.equipID,
                cancel: function (index, layero) {
                    flag = true;
                },
                end: function () {
                    if (!flag) {
                        table.reload(itemTable.id);
                    }
                }
            });
        });
        //双向发送次序排序
        $("#equipItemCommSort").on('click', function () {
            var flag = false;
            parent.layer.open({
                type: 2,
                title: ['双向发送次序排序调整'],
                area: ['1200px', '620px'],
                content: uxutil.path.ROOT + '/ui/layui/app/dic/equip/item/Comm_sort/app.html?' + config.equipID,
                cancel: function (index, layero) {
                    flag = true;
                },
                end: function () {
                    if (!flag) {
                        table.reload(itemTable.id);
                    }
                }
            });
        });
        //复制其他仪器项目
        $("#copyOtherEquipItem").on('click', function () {
            layer.open({
                type: 2,
                area: ['600px', '450px'],
                fixed: false,
                maxmin: false,
                title: '复制仪器项目',
                content: 'item/copy/index.html?equipid=' + config.equipID,
                cancel: function (index, layero) {
                    layer.close(index);
                }
            });
        });
    };
    //判断浏览器大小方法
    Class.pt.screen = function ($) {
        //获取当前窗口的宽度
        var width = $(window).width();
        if (width > 1200) {
            return 3;   //大屏幕
        } else if (width > 992) {
            return 2;   //中屏幕
        } else if (width > 768) {
            return 1;   //小屏幕
        } else {
            return 0;   //超小屏幕
        }
    };
	/***
	 * op="cache",则更新表格数据，将表格编辑控件数据更新到缓存table.cache[tableCacheId]; 
	 * op="form"，则从缓存table.cache[tableCacheId]获取数据更新表格对应的下拉框，日期等控件。
	 * op: 取值cache或者form。默认form
	 */
    Class.pt.updateCacheForm = function (tableId, tableCacheId, op) {
        var me = this;
        op = op || "form";
        var divForm = $(me.config.elem).next();
        var tableCache = table.cache[me.config.id];
        var trJqs = divForm.find(".layui-table-body tr");
        trJqs.each(function () {
            var trJq = $(this);
            var dataIndex = trJq.attr("data-index");
            trJq.find("td").each(function () {
                var tdJq = $(this);
                var fieldName = tdJq.attr("data-field");
                //var fieldName = selectJq.eq(0).attr("name");
                //更新select数据
                var selectJq = tdJq.find("select");
                if (selectJq.length == 1) {
                    if (op == "cache") {
                        tableCache[dataIndex][fieldName] = selectJq.eq(0).val();
                    } else if (op == "form") {
                        selectJq.eq(0).val(tableCache[dataIndex][fieldName])
                    }
                }
            });
        });
        return tableCache;
    };
    //检查是否存在相同的仪器通道标识--
    Class.pt.checkCompCode = function (obj) {//行数据
        var me = this;
        //备用或者null不用判断
        if (String(obj.LBEquipItemVO_LBEquipItem_IsReserve) == 'true' || obj.LBEquipItemVO_LBEquipItem_CompCode == null || obj.LBEquipItemVO_LBEquipItem_CompCode.toLowerCase() == 'null' || obj.LBEquipItemVO_LBEquipItem_CompCode == '') {
            return false;
        }
        //获得列表内容
        var tableCache = table.cache[me.config.id];
        for (var i = 0; i < tableCache.length; i++) {
            //备用或者null不用判断
            if (String(tableCache[i].LBEquipItemVO_LBEquipItem_IsReserve) == 'true' || tableCache[i].LBEquipItemVO_LBEquipItem_CompCode == null || obj.LBEquipItemVO_LBEquipItem_CompCode.toLowerCase() == 'null' || obj.LBEquipItemVO_LBEquipItem_CompCode == '') {
                continue;
            }
            if (obj.LBEquipItemVO_LBEquipItem_Id != tableCache[i].LBEquipItemVO_LBEquipItem_Id) {
                if (obj.LBEquipItemVO_LBEquipItem_CompCode == tableCache[i].LBEquipItemVO_LBEquipItem_CompCode) {
                    return true;
                }
            }
        }
        return false;
    }

    Class.pt.updateOne = function (index, obj) {
        var me = this;
        setTimeout(function () {
            var id = obj.LBEquipItemVO_LBEquipItem_Id;
            var PItemID = obj.LBEquipItemVO_LBEquipItem_PItemID;
            var SectionID = obj.LBEquipItemVO_LBEquipItem_SectionID;
            var IsReserve = obj.LBEquipItemVO_LBEquipItem_IsReserve;
            var CompCode = obj.LBEquipItemVO_LBEquipItem_CompCode;
            var entity = {
                Id: id,
                IsReserve: IsReserve,
                CompCode: CompCode
            };
            if (PItemID) entity.PItemID = PItemID;
            if (SectionID) entity.SectionID = SectionID;
            var fields = "Id,PItemID,SectionID,IsReserve,CompCode";
            var params = { entity: entity, fields: fields };
            params = JSON.stringify(params);
            //显示遮罩层
            var config = {
                type: "POST",
                url: me.config.editUrl,
                data: params
            };
            uxutil.server.ajax(config, function (data) {
                //隐藏遮罩层
                layer.closeAll('loading');
                if (data.success) {
                    me.saveCount++;
                } else {
                    me.saveErrorCount++;
                }
                if (me.saveCount + me.saveErrorCount == me.saveLength) {
                    if (me.saveErrorCount == 0) {
                        layer.msg('保存成功！', { icon: 1, time: 2000 });
                        table.reload(itemTable.id);
                    } else {
                        layer.msg('存在失败信息，具体错误内容请查看数据行的失败提示！', { icon: 5, anim: 6 });
                    }
                }
            })
        }, 100 * index);
    };
    //获取修改过的行记录
    Class.pt.getModifiedRecords = function () {
        var me = this, list = [];
        //获取列表数据
        var tableCache = table.cache[me.config.id];
        for (var i = 0; i < tableCache.length; i++) {
            //找到修改过数据的行
            if (tableCache[i].LBEquipItemVO_Tab) {
                list.push(tableCache[i]);
            }
        }
        return list;
    };
    //保存方法
    Class.pt.onSaveClick = function () {
        var me = this;
        var records = me.getModifiedRecords();
        if (records.length == 0) {
            layer.msg('没有修改的数据不需要保存！');
            return;
        }
        me.saveErrorCount = 0;
        me.saveCount = 0;
        me.saveLength = records.length;
        //显示遮罩
        if (records.length == 0) return;
        var indexs = layer.load();
        //获取列表数据
        for (var i = 0; i < records.length; i++) {
            //找到修改过数据的行
            if (records[i].LBEquipItemVO_Tab) {
                if (!me.checkCompCode(records[i])) {
                    me.updateOne(i, records[i]);
                } else {
                    layer.msg("通道标识：" + records[i].LBEquipItemVO_LBEquipItem_CompCode + '重复！');
                    layer.close(indexs);
                }
            }
        }
    };
    //暴露接口
    exports('itemTable', itemTable);
});
