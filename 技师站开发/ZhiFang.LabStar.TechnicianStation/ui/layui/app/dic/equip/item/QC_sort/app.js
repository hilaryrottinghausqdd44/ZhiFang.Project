/**
   @Name：项目选择
   @Author：zhangDa
   @version 2019-08-09
 */
layui.extend({
	uxutil: 'ux/util',
	sortlist:'app/dic/equip/item/QC_sort/list'
}).use(['uxutil','table','sortlist'],function(){
	var $ = layui.$,
		uxutil=layui.uxutil,
		sortlist = layui.sortlist,
		table = layui.table;
    var config={
    	//选择行数据
    	checkRowData:[],
        //获取选中的tr
        checkrow_tr:'',
        //是否手动调整
        IsAdjustment:false
    };
    //修改的字段
    var fields = {
        Id: "LBEquipItemVO_LBEquipItem_Id",
        DispOrder: "LBEquipItemVO_LBEquipItem_DispOrderQC"
    };
    var paramsObj = {
        searchWhere: null
    };
    getParams();
    //获得传递参数
    function getParams() {
        var params = location.search.split("?")[1];
        //参数赋值
        paramsObj.searchWhere = decodeURIComponent(params);
    }
    //项目列表功能参数配置
    var options={
    	elem:'#sortList',
    	id:'sortList',
    	title:'仪器项目质控排序',
        height: 'full-50',
        size: 'sm',
        where: { where: 'lbequip.Id=' + paramsObj.searchWhere },
		done: function(res, curr, count) {
			if(count>0) doAutoSelect();
		}
    };
    var list = sortlist.render(options);
    // 监听行单击事件
	table.on('row(sortList)', function(obj){
		config.checkrow_tr =this;
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	});
     //置顶
    $('#btntopup').on('click',function(){
    	onUpMove(2);
    });
    //向上
    $('#btnup').on('click',function(){
    	onUpMove(1);
    });
     //向下
    $('#btndown').on('click',function(){
    	onDownMove(1);
    });
     //置底
    $('#btnbottomdown').on('click',function(){
    	onDownMove(2);
    });
    //选中全部重新排序
    $('#btnreorder').on('click',function(){
    	reorder();
    });
    //清除选中排序
    $('#btnclorder').on('click', function () {
        clorder();
    });
  //手动调整
  $('#adjustment').on('click',function(){
  	config.IsAdjustment=true;
  	isShowBtn(false);
  });
  //手动调整完成
  $('#adjustmentfinish').on('click',function(obj){
  	config.IsAdjustment=false;
  	isShowBtn(true);
  });
	 //取消
    $('#cancel').on('click',function(){
    	var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	    parent.layer.close(index); //再执行关闭
    });
    //监听单元格事件
	table.on('tool(sortList)', function(obj){
	    var data = obj.data;
	    if(obj.event === 'setDispOrder'){
	    	if(config.IsAdjustment){
	    		$(this).find('input').focus();
	    		$(obj.tr).find(".layui-table-edit").keyup(function () {
	                var $input = $(this), val = $input.val();
	                if (!val) val = "0";
	                $input.val(val.replace(/[^\d]/g, ''));
	            });
	    	}else{
	    		$(this).find('input').blur();
	    	}
	    }else{
	    	$(this).find('input').blur();
	    }
	});
    //按钮可用
    var isShowBtn = function(bo){
    	if(bo){
    		$('#btntopup').removeClass('layui-btn-disabled').removeAttr('disabled',"true");
    		$('#btnup').removeClass('layui-btn-disabled').removeAttr('disabled',"true");
    		$('#btndown').removeClass('layui-btn-disabled').removeAttr('disabled',"true");
    		$('#btnbottomdown').removeClass('layui-btn-disabled').removeAttr('disabled',"true");
    	}else{
    	    $("#btntopup").prop("disabled", "disabled");
            $('#btntopup').addClass('layui-btn-disabled');
            
            $("#btnup").prop("disabled", "disabled");
            $('#btnup').addClass('layui-btn-disabled');
            
            $("#btndown").prop("disabled", "disabled");
            $('#btndown').addClass('layui-btn-disabled');
            
            $("#btnbottomdown").prop("disabled", "disabled");
            $('#btnbottomdown').addClass('layui-btn-disabled');
    	}
    };
    //默认选择第一行
    var doAutoSelect = function(){
    	//默认选择第一行
		var rowIndex  = 0;
		var tableDiv = $("#sortList+div .layui-table-body.layui-table-body.layui-table-main");
        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
        thatrow.click();
    };
    
    //重新排序
    var reorder = function(){
    	//从什么开始
    	var DispOrder = document.getElementById('DispOrder').value;
    	if(!DispOrder){
    		layer.msg("请输入显示次序！", { icon: 5, anim: 6 });
    		return;
        }
        var interval = document.getElementById('interval').value;
        if (!interval) {
            layer.msg("请输入显示次序间隔！", { icon: 5, anim: 6 });
            return;
        }
    	var tbData = table.cache['sortList'];
    	var checkStatus = table.checkStatus('sortList'),
            checkDatas= checkStatus.data;
    	for(var i=0;i<tbData.length;i++){
            var Id = tbData[i][fields.Id];
    		for(var j=0;j<checkDatas.length;j++){
                if (Id == checkDatas[j][fields.Id] ){
                    DispOrder = Number(DispOrder);
                    if (j > 0) DispOrder = DispOrder + Number(interval);
                    tbData[i][fields.DispOrder] =JSON.stringify(DispOrder);
    				break;
    			}
    		}
//  		DispOrder=Number(DispOrder);
//  		if(i>0)DispOrder=DispOrder+1;
//  	    tbData[i][fields.DispOrder] =JSON.stringify(DispOrder);
    	}
        table.reload('sortList', {
        	url:'',
            data: tbData
        });
        doAutoSelect();
    };
    //清除所有排序
    var clorder = function () {
		/* //从什么开始
		var DispOrder = document.getElementById('DispOrder').value;
		if(!DispOrder){
			layer.msg("请输入显示次序！", { icon: 5, anim: 6 });
			return;
	    }
	    var interval = document.getElementById('interval').value;
	    if (!interval) {
	        layer.msg("请输入显示次序间隔！", { icon: 5, anim: 6 });
	        return;
	    } */
        var tbData = table.cache['sortList'];
        var checkStatus = table.checkStatus('sortList'),
            checkDatas = checkStatus.data;
        if (checkDatas.length == 0) {
            layer.msg("请选中再进行清除排序！");
            return;
        }
        for (var i = 0; i < tbData.length; i++) {
            var Id = tbData[i][fields.Id];
            for (var j = 0; j < checkDatas.length; j++) {
                if (Id == checkDatas[j][fields.Id]) {
                    var DispOrder = 0;
                    tbData[i][fields.DispOrder] = JSON.stringify(DispOrder);
                    break;
                }
            }
        }
        table.reload('sortList', {
            url: '',
            data: tbData
        });
        doAutoSelect();
    };
     /**向上移动方法  
     * type=1 向下移动一行
     * type=2 置底
     * */
    var onUpMove = function (type) {
        //当前选择行
        var tr = $(config.checkrow_tr);
        var tbData = table.cache['sortList'];
        if (tr == null) {
            layer.msg("请选择元素");
            return;
        }
        if ($(tr).prev().html() == null) {
            layer.msg("已经是最顶部了");
            return;
        } else {
            //当前选择行数据
            var tem = tbData[tr.index()];
            var selectindex = 0;
            //向上
            if (type == 1) {
                //当前选择上的上一行数据
                var tem2 = tbData[tr.prev().index()];
                // 将本身插入到目标tr之前
                $(tr).insertBefore($(tr).prev());
                //获取本行显示次序
                var DispOrder = tem[fields.DispOrder];
                //下一行显示次序
                var nDispOrder = tem2[fields.DispOrder];
                if (!DispOrder) DispOrder = 0;
                if (!nDispOrder) nDispOrder = 0;
                //临时变量
                var temp = DispOrder;
                //双方显示次序互换
                tem[fields.DispOrder] = Number(nDispOrder);//本行
                tem2[fields.DispOrder] = Number(temp);//下一行

                // 上移之后，数据交换
                tbData[tr.index()] = tem;
                tbData[tr.next().index()] = tem2;
                selectindex = tr.index();
            } else {
                //删除当前选择行数据
                tbData.splice(tr.index(), 1);
                //获取第一行的显示次序
                var DispOrder = tbData[0][fields.DispOrder];
                tem[fields.DispOrder] = Number(DispOrder) - 1;
                tbData.splice(0, 0, tem);
                selectindex = 0;
            }
            table.reload('sortList', {
                url: '',
                data: tbData
            });
            var tableDiv = $("#sortList+div .layui-table-body.layui-table-body.layui-table-main");
            var thatrow = tableDiv.find('tr:eq(' + selectindex + ')');
            thatrow.click();
        }
    };
    /**向下移动方法  
     * type=1 向下移动一行
     * type=2 置底
     * */
    var onDownMove = function (type) {
        //当前选择行
        var tr = $(config.checkrow_tr);
        var tbData = table.cache['sortList'];
        if (tr == null) {
            layer.msg("请选择行");
            return;
        }
        if ($(tr).next().html() == null) {
            layer.msg("已经是最底部了");
            return;
        } else {
            //选择行数据本行
            var tem = tbData[tr.index()];
            if (type == 1) {
                //选择行的下一行数据
                var tem2 = tbData[tr.next().index()];
                //获取本行显示次序
                var DispOrder = tem[fields.DispOrder];
                //下一行显示次序
                var nDispOrder = tem2[fields.DispOrder];
                if (!DispOrder) DispOrder = 0;
                if (!nDispOrder) nDispOrder = 0;
                var temp = DispOrder;
                //双方显示次序互换
                tem[fields.DispOrder] = Number(nDispOrder);//本行
                tem2[fields.DispOrder] = Number(temp);//上一行
                // 将本身插入到目标tr的后面
                $(tr).insertAfter($(tr).next());
                // 交换数据
                tbData[tr.index()] = tem;
                tbData[tr.prev().index()] = tem2;
                var selectindex = tr.index();

            } else {
                //删除当前选择行数据
                tbData.splice(tr.index(), 1);
                //获取最后一行索引
                var lasslen = tbData.length - 1;
                //获取最后一行数据
                var tem2 = tbData[lasslen];
                //获取最后一行的显示次序
                var DispOrder = tem2[fields.DispOrder];
                tem[fields.DispOrder] = Number(DispOrder) + 1;
                tbData.push(tem);
                if (tbData.length == 0) tbData.length = 1;
                var selectindex = tbData.length - 1;
            }
            table.reload('sortList', {
                url: '',
                data: tbData
            });
            var tableDiv = $("#sortList+div .layui-table-body.layui-table-body.layui-table-main");
            var thatrow = tableDiv.find('tr:eq(' + selectindex + ')');
            thatrow.click();
        }
    };
});