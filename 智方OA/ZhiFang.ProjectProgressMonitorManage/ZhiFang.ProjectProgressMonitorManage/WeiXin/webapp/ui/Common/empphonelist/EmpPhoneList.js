$(function () {
    //页面所有功能对象
    var shell_win = {
        selectempid: [],
        selectempname: [],
        dataList:[],
        Load: function () {
            if (shell_win.selectempid.length > 0) {
                for (var i = 0; i < shell_win.selectempid.length; i++) {
                    //alert(shell_win.selectempid[i]);
                    if ($("#checked" + shell_win.selectempid[i]).attr("src") == "../img/icon/unchecked.png") {
                        $("#checked" + shell_win.selectempid[i]).attr("src", "../img/icon/checked.png");
                    }
                }
            }
        },
        Submit: function () {
            history.back();
        },
        btnsearchWords:  function (){
	    	var val= $("#searchWords").val();
	    	var arr=dataList.value.list;
	    	var list = [],result = {};
	    	for(var i = 0;i< arr.length; i++) {
	    		var MobileTel=arr[i].HREmployee_MobileTel;
	    		if(MobileTel.indexOf(val) >= 0){
	    			var obj = {
	    			    HREmployee_MobileTel:arr[i].HREmployee_MobileTel,
	    			    HREmployee_HRDept_CName:arr[i].HREmployee_HRDept_CName,
	    			    HREmployee_CName:arr[i].HREmployee_CName,
	    			    HREmployee_Id:arr[i].HREmployee_Id
	    			};
	    			list.push(obj);	
	    		}
            }
	    	result.list = list;
	    	search(result.list);
	    },	
	    keyup:function(){
	    	 $(this).change();
	    },
        setcheckstatus: function (empidid) {
            //alert(1);
            if ($("#checked" + empidid)) {
                //alert(2);
                if ($("#checked" + empidid).attr("src") == "../img/icon/unchecked.png") {
                    var indexid = jQuery.inArray(empidid, shell_win.selectempid)
                    if (indexid < 0) {
                        //alert(3);
                        shell_win.selectempid.push(empidid);
                        // alert(4);
                        if ($("#empname" + empidid)) {
                            //alert(5);
                            shell_win.selectempname.push($("#empname" + empidid).text())
                        }
                    }
                    //alert(6);
                    $("#checked" + empidid).attr("src", "../img/icon/checked.png");
                    //alert(7);

                }
                else {
                    // alert(jQuery.inArray(empidid, shell_win.selectempid));//是b这个元素在数组arrList 中的位置  splice(index,1)
                    var indexid = jQuery.inArray(empidid, shell_win.selectempid)
                    if (indexid >= 0) {
                        shell_win.selectempid.splice(indexid, 1)
                    }
                    if ($("#empname" + empidid)) {
                        var indexname = jQuery.inArray($("#empname" + empidid).text(), shell_win.selectempname)
                        if (indexname >= 0) {
                            shell_win.selectempname.splice(indexname, 1)
                        }
                    }
                    $("#checked" + empidid).attr("src", "../img/icon/unchecked.png");
                }
            }
        },
     
        /**初始化*/
        init: function () {
            if (localStorage.getItem("CopyForEmpIdList")) {
                shell_win.selectempid = localStorage.getItem("CopyForEmpIdList").split(',');
            }

            if (localStorage.getItem("CopyForEmpNameList")) {
                shell_win.selectempname = localStorage.getItem("CopyForEmpNameList").split(',');
            }
            $("#submitspan").on(Shell.util.Event.touch, shell_win.Submit);
            $("#searchWords").on('change', shell_win.btnsearchWords);
            $("#searchWords").on('keyup', shell_win.keyup);
            shell_win.Load();
        }
    };
    shell_win.init();
  
    //加载订货单列表数据
    function loadData(callback) {
        var url = Shell.util.Path.rootPath + '/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true';
        var fields = [
			'HREmployee_MobileTel', 'HREmployee_HRDept_CName',
			'HREmployee_CName', 'HREmployee_Id'
        ];
        url += '&fields=' + fields.join(',');
        url += '&where=' + getWhere();
        ShellComponent.mask.loading();
        Shell.util.Server.ajax({
            async: true,
            url: url
        }, function (data) {
        	dataList=data;
            ShellComponent.mask.hide();
            callback(data);
        });
    }
    //获取条件
    function getWhere() {
        return "1=1 and hremployee.IsUse=true ";
    }
    //创建列表内容
    function createList(data) {
        var html = ['<div id="letter"></div><div class="sort_box">'];
        if (!data.success) {
            html.push('<div class="error-div">' + data.msg + '</div>');//错误信息
        } else {
            var list = data.value.list || [],
				len = list.length;
            for (var i = 0; i < len; i++) {
                var row = createRow(list[i]);
                html.push(row);
            }

            if (len == 0) {
                html.push('<div class="no-data-div">没有找到数据!</div>');//没有数据
            }
        }
        html.push('</div>');
        html.push( " <div class=\"initials\"><ul> <li style=\"height: 15px\">A</li><li style=\"height: 15px\">B</li><li style=\"height: 15px\">C</li><li style=\"height: 15px\">D</li><li style=\"height: 15px\">E</li><li style=\"height: 15px\">F</li><li style=\"height: 15px\">G</li><li style=\"height: 15px\">H</li><li style=\"height: 15px\">I</li><li style=\"height: 15px\">J</li><li style=\"height: 15px\">K</li><li style=\"height: 15px\">L</li><li style=\"height: 15px\">M</li><li style=\"height: 15px\">N</li><li style=\"height: 15px\">O</li><li style=\"height: 15px\">P</li><li style=\"height: 15px\">Q</li><li style=\"height: 15px\">R</li><li style=\"height: 15px\">S</li><li style=\"height: 15px\">T</li><li style=\"height: 15px\">U</li><li style=\"height: 15px\">V</li><li style=\"height: 15px\">W</li><li style=\"height: 15px\">X</li><li style=\"height: 15px\">Y</li><li style=\"height: 15px\">Z</li><li style=\"height: 15px\">#</li></ ul ></ div > ");
        return html.join('');
    }
  
    //创建数据行内容
    function createRow(value) {
        var html = [];
        var id = value.HREmployee_Id;
        var CName = value.HREmployee_CName;
        var HRDepCName = value.HREmployee_HRDept_CName;
        var MobileTel = value.HREmployee_MobileTel;

        var logo = "<div class=\"num_logo\"><img src=\"../../img/icon/user.png\" /></div>";
        var phone = value != null && MobileTel != null ? "<br>电话：" +MobileTel : "";
        var deptname = value != null && HRDepCName!= null && HRDepCName != null ? "(" +HRDepCName + ")" : "";
        var empname = "<div id = \"empname" + id + "\" class=\"num_name\" >" + CName + deptname + phone + "</div>";
        html.push( "<div id=\"sort_list_" + id + "\" empid=\"" + id + "\"  class=\"sort_list\" >" + logo + empname + "</div>");
        return html.join('');
    }

    //刷新列表数据
    function refreshContent() {
        //加载数据
        loadData(function (data) {
            var html = createList(data);
            $("#ContentDiv").html(html);
            SortList();
        });

    }
    //刷新数据
    setTimeout(function () {
        refreshContent();
    }, 500);
    // 本地过滤根据姓名过滤
    function search(list) {
        var html = ['<div id="letter"></div><div class="sort_box">'];
        if (!list) {
            html.push('<div class="error-div">' + data.msg + '</div>');//错误信息
        } else {
            var list = list || [],
				len = list.length;
            for (var i = 0; i < len; i++) {
                var row = createRow(list[i]);
                html.push(row);
            }

            if (len == 0) {
                html.push('<div class="no-data-div">没有找到数据!</div>');//没有数据
            }
        }
         html.push('</div>');

         html.push( " <div class=\"initials\"><ul> <li style=\"height: 15px\">A</li><li style=\"height: 15px\">B</li><li style=\"height: 15px\">C</li><li style=\"height: 15px\">D</li><li style=\"height: 15px\">E</li><li style=\"height: 15px\">F</li><li style=\"height: 15px\">G</li><li style=\"height: 15px\">H</li><li style=\"height: 15px\">I</li><li style=\"height: 15px\">J</li><li style=\"height: 15px\">K</li><li style=\"height: 15px\">L</li><li style=\"height: 15px\">M</li><li style=\"height: 15px\">N</li><li style=\"height: 15px\">O</li><li style=\"height: 15px\">P</li><li style=\"height: 15px\">Q</li><li style=\"height: 15px\">R</li><li style=\"height: 15px\">S</li><li style=\"height: 15px\">T</li><li style=\"height: 15px\">U</li><li style=\"height: 15px\">V</li><li style=\"height: 15px\">W</li><li style=\"height: 15px\">X</li><li style=\"height: 15px\">Y</li><li style=\"height: 15px\">Z</li><li style=\"height: 15px\">#</li></ ul ></ div > ");
        $("#ContentDiv").html(html);
        SortList();
    }
    function SortList(){
        var Initials=$('.initials');
        var LetterBox=$('#letter');
        initials();
        $(".initials ul li").click(function(){
            var _this=$(this);
            var LetterHtml=_this.html();
            LetterBox.html(LetterHtml).fadeIn();

            Initials.css('background','rgba(145,145,145,0.6)');
            
            setTimeout(function(){
                Initials.css('background','rgba(145,145,145,0)');
                LetterBox.fadeOut();
            },1000);

            var _index = _this.index()
            if(_index==0){
                $('html,body').animate({scrollTop: '0px'}, 300);//点击第一个滚到顶部
            }else if(_index==27){
                var DefaultTop=$('#default').position().top;
                $('html,body').animate({scrollTop: DefaultTop+'px'}, 300);//点击最后一个滚到#号
            }else{
                var letter = _this.text();
                if($('#'+letter).length>0){
                    var LetterTop = $('#'+letter).position().top;
                    $('html,body').animate({scrollTop: LetterTop-45+'px'}, 300);
                }
            }
        })
    }
    //公众号排序
    function initials(){
    	var SortList=$(".sort_list");
	    var SortBox=$(".sort_box");
	    SortList.sort(asc_sort).appendTo('.sort_box');//按首字母排序
	    function asc_sort(a, b) {
	        return makePy($(b).find('.num_name').text().charAt(0))[0].toUpperCase() < makePy($(a).find('.num_name').text().charAt(0))[0].toUpperCase() ? 1 : -1;
	    }
	
	    var initials = [];
	    var num=0;
	    SortList.each(function(i) {
	        var initial = makePy($(this).find('.num_name').text().charAt(0))[0].toUpperCase();
	        if(initial>='A'&&initial<='Z'){
	            if (initials.indexOf(initial) === -1)
	                initials.push(initial);
	        }else{
	            num++;
	        }
	    });
	
	    $.each(initials, function(index, value) {//添加首字母标签
	        SortBox.append('<div class="sort_letter" id="'+ value +'">' + value + '</div>');
	    });
	    if(num!=0){SortBox.append('<div class="sort_letter" id="default">#</div>');}
	
	    for (var i =0;i<SortList.length;i++) {//插入到对应的首字母后面
	        var letter=makePy(SortList.eq(i).find('.num_name').text().charAt(0))[0].toUpperCase();
	        switch(letter){
	            case "A":
	                $('#A').after(SortList.eq(i));
	                break;
	            case "B":
	                $('#B').after(SortList.eq(i));
	                break;
	            case "C":
	                $('#C').after(SortList.eq(i));
	                break;
	            case "D":
	                $('#D').after(SortList.eq(i));
	                break;
	            case "E":
	                $('#E').after(SortList.eq(i));
	                break;
	            case "F":
	                $('#F').after(SortList.eq(i));
	                break;
	            case "G":
	                $('#G').after(SortList.eq(i));
	                break;
	            case "H":
	                $('#H').after(SortList.eq(i));
	                break;
	            case "I":
	                $('#I').after(SortList.eq(i));
	                break;
	            case "J":
	                $('#J').after(SortList.eq(i));
	                break;
	            case "K":
	                $('#K').after(SortList.eq(i));
	                break;
	            case "L":
	                $('#L').after(SortList.eq(i));
	                break;
	            case "M":
	                $('#M').after(SortList.eq(i));
	                break;
	            case "O":
	                $('#O').after(SortList.eq(i));
	                break;
	            case "P":
	                $('#P').after(SortList.eq(i));
	                break;
	            case "Q":
	                $('#Q').after(SortList.eq(i));
	                break;
	            case "R":
	                $('#R').after(SortList.eq(i));
	                break;
	            case "S":
	                $('#S').after(SortList.eq(i));
	                break;
	            case "T":
	                $('#T').after(SortList.eq(i));
	                break;
	            case "U":
	                $('#U').after(SortList.eq(i));
	                break;
	            case "V":
	                $('#V').after(SortList.eq(i));
	                break;
	            case "W":
	                $('#W').after(SortList.eq(i));
	                break;
	            case "X":
	                $('#X').after(SortList.eq(i));
	                break;
	            case "Y":
	                $('#Y').after(SortList.eq(i));
	                break;
	            case "Z":
	                $('#Z').after(SortList.eq(i));
	                break;
	            default:
	                $('#default').after(SortList.eq(i));
	                break;
	        }
	    }
    }
});