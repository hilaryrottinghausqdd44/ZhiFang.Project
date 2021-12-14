/**
   @Name：检验确认设置
   @Author：GHX
   @version 2021-05-08
 */
layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'element', 'layer'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
        uxutil = layui.uxutil;
    var app = {};
	//get参数
	app.paramsObj = {
		sectionId: null, //小组ID
		sectionCName: null,//小组名称
		tabtype:0
	};
    //初始化
    app.init = function () {
        var me = this;
        me.getParams();
        me.initListeners();
		me.inittab();
    };
	app.inittab = function(){
		var me = this;
		element.tabChange('Tab', 'testconfirm_tab_title'+app.paramsObj.tabtype);
	}
	//获得参数
	app.getParams = function() {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.SECTIONID) {
			me.paramsObj.sectionId = params.SECTIONID;
		}
		if (params.SECTIONCNAME) {
			me.paramsObj.sectionCName = params.SECTIONCNAME;
		}
		if (params.TABTYPE) {
			me.paramsObj.tabtype = params.TABTYPE;
		}
	};
    //设置dom元素高度
    app.setDomHeight = function () {
        var me = this;
        //设置iframe父元素高度
        $(".layui-tab-content").css("height", ($(window).height() - 62) + "px");
    };
    //监听
    app.initListeners = function () {
        var me = this;
        // 窗体大小改变时，调整高度显示
        /* $(window).resize(function () {
            clearTimeout(iTime);
            iTime = setTimeout(function () {
                me.setDomHeight();
            }, 500);
        }); */
        element.on('tab(Tab)', function (data) {
		   if(data.index == 0){//检验确认人
			   //检验确认人弹出Handler,审核人弹出Checker,保存到内存时用
			   var OperateType = 'Handler';
			   //授权操作类型枚举,检验确认人的Name='检验确认',审核人的Name='审核'
			   var OperateTypeText='检验确认';
			   //授权操作类型枚举，检验确认人的Id='2',审核人的Id='1'
			   var  OperateTypeID='2';
			   var url = uxutil.path.ROOT + '/ui/layui/views/sample/setting/basic/info.html?SectionID='+me.paramsObj.sectionId;
			   url += "&OperateType=Handler&OperateTypeText=检验确认&OperateTypeID=2";
			   me.openTabsPage(url);
		   }else if (data.index == 1){//审核人设置
			  //检验确认人弹出Handler,审核人弹出Checker,保存到内存时用
			   var OperateType = 'Checker';
			   //授权操作类型枚举,检验确认人的Name='检验确认',审核人的Name='审核'
			   var OperateTypeText='审核';
			   //授权操作类型枚举，检验确认人的Id='2',审核人的Id='1'
			   var  OperateTypeID='1'; 
			   var url = uxutil.path.ROOT + '/ui/layui/views/sample/setting/basic/info.html?SectionID='+me.paramsObj.sectionId;
			   url += "&OperateType=Checker&OperateTypeText=审核&OperateTypeID=1";
			   me.openTabsPage(url);
		   } else if (data.index == 2) {//智能审核
			   var url = uxutil.path.ROOT + '/ui/layui/views/sample/setting/judge/judge.html?SectionID=' + me.paramsObj.sectionId + "&sectionCName=" + me.paramsObj.sectionCName;
			    me.openTabsPage(url);
		   }
        });
    };    
	//打开标签页
	app.openTabsPage = function (url) {
		var str = '<div class="layui-tab-item layui-show"><iframe id=""  src=' + url + '  style="border: medium none;overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:80%;width:98.5%;position:absolute;"></iframe></div>';
		$('#testconfirm_tab_content').html(str);
	}
    //初始化
    app.init();
});