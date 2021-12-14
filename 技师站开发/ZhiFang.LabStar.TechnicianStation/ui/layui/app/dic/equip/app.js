/**
   @Name：仪器
   @Author：zhangda
   @version 2019-08-15
 */
layui.extend({
    uxutil: 'ux/util',
    equipTable: 'app/dic/equip/equipTable',
    equipForm: 'app/dic/equip/equipForm',
    itemTable: 'app/dic/equip/item/itemTable',
    resultTable:'app/dic/equip/result/resultTable',
    equipsectiontab:'app/dic/equip/equipsection/index'
}).use(['uxutil', 'table', 'form', 'element', 'equipTable', 'equipForm', 'itemTable','resultTable','equipsectiontab'], function () {
    var $ = layui.$,
        element = layui.element,
        form = layui.form,
        uxutil = layui.uxutil,
        equipTable = layui.equipTable,
        equipForm = layui.equipForm,
        itemTable = layui.itemTable,
        resultTable = layui.resultTable,
        equipsectiontab = layui.equipsectiontab,
        table = layui.table;
    //全局变量
    var config = {
        //当前选择行数据
        checkRowData: [],
        //当前激活页签,默认表单,自定义变量0-表单，1-仪器项目，2-打印格式，3-历史前值对比，4-常用值短语维护
        currTabIndex: 0,
        //已激活页签，用于判断页签是否已加载
        isLoadTabArr: []
    };
    //仪器特定小组
    var equipsection = null;
    //初始化
    init();
    //初始化
    function init() {
        //表单高度
        $(".tableHeight").css("height", ($(window).height() - 15) + "px");//设置表单容器高度
        $(".container").css("height", (parseFloat($(".tableHeight").css("height")) - 100) + "px");
        //仪器列表功能参数配置
        var options = {
            elem: '#table',
            id: 'table',
            title: '仪器',
            height: 'full-65',
            defaultOrderBy: JSON.stringify([{ property: 'LBEquip_DispOrder', direction: 'ASC' }])
        };
        equipTable.render(options);
        //监听联动
        initGroupListeners();
    }
    //仪器维护联动
    function initGroupListeners() {
        // 窗体大小改变时，调整高度显示
        $(window).resize(function () {
            var width = $(this).width();
            var height = $(this).height();
            //表单高度
            var tabHeight = ($(window).height() - 15) + "px";
            $('.tableHeight').css("height", tabHeight);
            $(".container").css("height", (parseFloat($(".tableHeight").css("height")) - 100) + "px");
        });

        //仪器列表行 监听行单击事件
        table.on('row(table)', function (obj) {
            config.checkRowData = [];
            config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            onSearch(config.checkRowData);
        });

        //仪器列表 工具栏
        table.on('toolbar(table)', function (obj) {
            var checkStatus = table.checkStatus(obj.config.id);
            switch (obj.event) {
                case 'del':
                    equipTable.onDelClick(config.checkRowData);
            };
        });

        //列表数据加载完成后
        layui.onevent("table", "done", function (obj) {
            //没有数据时，清空表单数据
            if (obj.count == 0) {
                $('#formType').addClass("layui-hide");
                config.checkRowData = [];
                equipForm.onReset();
            }
        });

        //监听查询，仪器列表
        form.on('submit(search)', function (data) {
            var hql = '';
            //模糊查询
            if (data.field.fuzzyQuery) {
                hql = "(CName like '%" + data.field.fuzzyQuery +
                    "%' or Computer like '%" + data.field.fuzzyQuery + "%' or SName like '%" +
                    data.field.fuzzyQuery + "%' or ComProgram like '%" +
                    data.field.fuzzyQuery + "%')";
            }
            equipTable.onSearch(hql);
        });

        //监听回车按下事件--查询项目
        $(document).keydown(function (event) {
            switch (event.keyCode) {
                case 13:
                    //判断焦点是否在该输入框
                    if (document.activeElement == document.getElementById("fuzzyQuery")) {
                        $("#search").click();
                    }
            }
        });

        //表单保存后处理
        layui.onevent("SaveLBEquipForm", "save", function (obj) {
            var formtype = obj.formtype;
            var msg = '';
            if (formtype == 'add') msg = '新增成功!';
            else msg = '修改成功!';
            equipTable.config.PK = obj.id;
            layer.msg(msg, { icon: 1, time: 2000 });
            table.reload('table', {});
        });

        //表单按钮删除成功后刷新仪器列表
        layui.onevent("DelLBEquipForm", "del", function (obj) {
            equipTable.setDelIndex();
            table.reload('table', {});
        });
        //编辑
        $('#edit').on('click', function () {
            if (config.checkRowData.length > 0) {
                var id = config.checkRowData[0].LBEquip_Id;
                equipForm.isEdit(id);
            }
        });
        //仪器排序
        $('#sort').on('click', function () {
            //获得查询条件
            var fuzzyQuery = document.getElementById("fuzzyQuery").value;
            //var hql = "1=1";
            //if (fuzzyQuery != "") {
            //    hql = "(CName like '%" + fuzzyQuery +
            //        "%' or Computer like '%" + fuzzyQuery + "%' or SName like '%" +
            //        fuzzyQuery + "%' or ComProgram like '%" +
            //        fuzzyQuery + "%')";
            //}
            //打开排序页面
            var flag = false;
            parent.layer.open({
                type: 2,
                title: ['仪器排序调整'],
                area: ['1200px', '620px'],
                content: uxutil.path.ROOT + '/ui/layui/app/dic/equip/sort/app.html?' + fuzzyQuery,
                cancel: function (index, layero) {
                    flag = true;
                },
                end: function () {
                    if (!flag) {
                        table.reload('table', {});
                    }
                }
            });
        });
         //通讯控制参数
        $('#commpara').on('click', function () {
        	var win = $(window),
				maxWidth = win.width()-100,
				maxHeight = win.height()-80,
				width = maxWidth > 800 ? maxWidth : 800,
				height = maxHeight > 600 ? maxHeight : 600;
            layer.open({
	            type: 2,
	            area:[width+'px',height+'px'],
	            fixed: false,
	            maxmin: false,
	            title:'通讯控制参数',
	            content: '../../../views/system/comm/para/index.html?equipid='+config.checkRowData[0].LBEquip_Id,
	            cancel: function (index, layero) {
		        	parent.layer.closeAll('iframe');
	            }
	        });
        });
        //导入
        $("#import").on('click', function () {
            layer.open({
                type: 2,
                title:'选择导入文件',
                area: ['400px', '160px'],
                content: uxutil.path.LAYUI + '/views/system/comm/import/file/index.html?ENTITYNAME=LBEquip',
                success: function (layero, index) {
                    //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
                    var iframeWin = window[layero.find('iframe')[0]['name']];
                    iframeWin.externalCallFun(function () { table.reload('table', {}); layer.close(index); });
                }
            });
        });
    }
    element.on('tab(tab)', function (obj) {
        config.currTabIndex = obj.index;
        var isLoad = false;
        //上一个选择的仪器
        var equipId = '';
        //判断当前页签是否已加载过数据
        for (var i = 0; i < config.isLoadTabArr.length; i++) {
            if (config.isLoadTabArr[i].index == obj.index) {
                equipId = config.isLoadTabArr[i].curRowId;
                isLoad = true;
                break;
            }
        }
        switch (config.currTabIndex) {
            case 0:
                //仪器表单
                break;
            case 1://仪器项目维护
                if (!isLoad) {
                    iniItemList();
                    var obj1 = { index: obj.index, curRowId: config.checkRowData[0].LBEquip_Id };
                    config.isLoadTabArr.push(obj1);
                }
                break;
            case 2://打印格式
                //if (!isLoad) {
                //    var obj1 = { index: obj.index, curRowId: config.checkRowData[0].LBEquip_Id };
                //    config.isLoadTabArr.push(obj1);
                //}
                break;
            case 3://仪器结果替换
                if (!isLoad) {
                    iniResultList();
                    var obj1 = { index: obj.index, curRowId: config.checkRowData[0].LBEquip_Id };
                    config.isLoadTabArr.push(obj1);
                }
                break;
            case 4://仪器特定小组
                if (!isLoad) {
                     //仪器特定小组列表功能参数配置
                    equipsection = equipsectiontab.render({});
                    var obj1 = { index: obj.index, curRowId: config.checkRowData[0].LBEquip_Id };
                    config.isLoadTabArr.push(obj1);
                }
                break;   
            default:
                break;
        }
        if (equipId != config.checkRowData[0].LBEquip_Id) {
            onSearch();
        }
    });
    function onSearch(recs) {
        for (var i = 0; i < config.isLoadTabArr.length; i++) {
            //当前页签
            if (config.isLoadTabArr[i].index == config.currTabIndex) {
                config.isLoadTabArr.splice(i, 1); //删除下标为i的元素
                var obj1 = { index: config.currTabIndex, curRowId: config.checkRowData[0].LBEquip_Id };
                config.isLoadTabArr.push(obj1);
            }
        }
        //初始化，默认页签为表单页签
        if (config.isLoadTabArr.length == 0) {
            var obj1 = { index: config.currTabIndex, curRowId: config.checkRowData[0].LBEquip_Id };
            config.isLoadTabArr.push(obj1);
        }
        setTimeout(function () {
            switch (config.currTabIndex) {
                case 0://表单
                    equipForm.isShow(config.checkRowData[0].LBEquip_Id);
                    break;
                case 1://仪器项目维护
                    //var where ='LBEquip.Id='+config.checkRowData[0].LBEquip_Id;
                    itemTable.onSearch('itemtable', config.checkRowData[0]);
                    break;
                case 2://打印格式
                    break;
                case 3://仪器结果替换
                    resultTable.onSearch('resultTable', config.checkRowData[0]);
                    break;
                case 4://仪器特定小组
                    if(config.checkRowData[0].LBEquip_Id)equipsection.loadData(config.checkRowData[0].LBEquip_Id);
                    break;
                default:
                    break;
            }
        }, 200);
    }
    //仪器项目渲染’
    function iniItemList() {
        //仪器项目列表功能参数配置
        var itemObj = {
            elem: '#itemtable',
            title: '仪器项目',
            height: 'full-100',
            id: 'itemtable'
        };
        itemTable.render(itemObj);
    }
    //仪器结果替换渲染’
    function iniResultList() {
        //仪器项目列表功能参数配置
        var itemObj = {
            elem: '#resultTable',
            title: '仪器结果替换',
            height: 'full-100',
            id: 'resultTable'
        };
        resultTable.render(itemObj);
    }
});