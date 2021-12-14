/**
   @Name：复制仪器项目
   @Author：zhangda
   @version 2021-11-23
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable: 'ux/table',
	uxbase: 'ux/base',
}).use(['uxutil', 'table','uxtable', 'uxbase'], function () {
	var $ = layui.$,
        uxutil = layui.uxutil,
        uxtable = layui.uxtable,
		uxbase = layui.uxbase,
		table = layui.table;
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//当前仪器
	var EQUIPID = PARAMS.EQUIPID || null;
	//仪器列表实例
    var EquipTableInstance = null;
    //仪器列表ID
    var EquipTableID = 'equiptable';
    //当前选择行
    var CheckRowData = [];
    //获取仪器服务地址
    var GET_EQUIP_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true';
    //复制仪器项目服务地址
    var COPY_EQUIP_ITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_LBEquipItemCopyByEquipID';

	//初始化仪器列表
    function initEquipTable() {
        var url = GET_EQUIP_URL + "&where=IsUse=1 and Id <> " + EQUIPID + "&fields=LBEquip_Id,LBEquip_EquipNo,LBEquip_CName,LBEquip_SName,LBEquip_DispOrder";
        url += '&sort=[{"property":"LBEquip_DispOrder","direction":"ASC"}]';
		EquipTableInstance = uxtable.render({
            elem: "#" + EquipTableID,
            id: EquipTableID,
            toolbar: '',
            url: url,
            page: false,
            limit: 1000,
            autoSort: true, //禁用前端自动排序
            loading: true,
            size: 'sm', //小尺寸的表格
            height: 'full-60',
            cols: [[
                { type: 'numbers', title: '行号' },
                { field: 'LBEquip_Id', width: 60, title: '主键ID', sort: true, hide: true },
                { field: 'LBEquip_EquipNo', minWidth: 80, title: '编码', sort: true, hide: true },
                { field: 'LBEquip_CName', minWidth: 150, flex: 1, title: '名称', sort: true },
                { field: 'LBEquip_SName', width: 150, title: '简称', sort: true },
                { field: 'LBEquip_DispOrder', width: 100, title: '显示次序', sort: true, hide: true }
            ]],
            text: { none: '暂无相关数据' },
            parseData: function (res) {//res即为原始返回的数据
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                if (count > 0) {
                    if ($("#" + EquipTableID + "+div .layui-table-body table.layui-table tbody tr:first-child")[0]) {
                        setTimeout(function () {
                            $("#" + EquipTableID + "+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
                        }, 0);
                    }
                }
            }
		});
    };
    //确认按钮处理
    function onSaveClick() {
        if (CheckRowData.length == 0) {
            layer.msg('请选择要复制的仪器！', { icon: 0, anim: 0 });
            return;
        }

        var url = COPY_EQUIP_ITEM_URL + '?fromEquipID=' + CheckRowData[0]["LBEquip_Id"] + '&toEquipID=' + EQUIPID;
        var index = layer.load();
        uxutil.server.ajax({
            url: url
        }, function (data) {
            //隐藏遮罩层
            layer.close(index);
            if (data.success) {
                parent.layui.itemTable.onRefresh();
                parent.layer.msg('复制成功！', { icon: 6, anim: 0, time: 2000 });
                var index2 = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                parent.layer.close(index2); //再执行关闭
            } else {
                layer.msg(data.ErrorInfo, { icon: 5, anim: 0 });
            }
        })
    }

	//初始化监听
    function initListeners() {
        //监听样本单列表单击
        table.on('row(' + EquipTableID + ')', function (obj) {
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            CheckRowData = [];
            CheckRowData.push(obj["data"]);
        });
        //确定按钮处理
        $("#save").on('click', function () {
            onSaveClick();
        });
        //取消按钮处理
        $("#cancel").on('click', function () {
            var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
            parent.layer.close(index); //再执行关闭
        });
	};
	//初始化页面
	function initHtml() {
        initEquipTable();
        initListeners();
	};

	initHtml();
});