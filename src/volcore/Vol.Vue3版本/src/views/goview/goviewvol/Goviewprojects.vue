<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/goview/goviewvol/Goviewprojects.js此处编写
 -->
<template>
    <view-grid ref="grid"
               :columns="columns"
               :detail="detail"
               :editFormFields="editFormFields"
               :editFormOptions="editFormOptions"
               :searchFormFields="searchFormFields"
               :searchFormOptions="searchFormOptions"
               :table="table"
               :extend="extend">
    </view-grid>
</template>
<script>
    import extend from "@/extension/goview/goviewvol/Goviewprojects.js";
    import { ref, defineComponent } from "vue";
    export default defineComponent({
        setup() {
            const table = ref({
                key: 'Id',
                footer: "Foots",
                cnName: '大屏项目',
                name: 'goviewvol/Goviewprojects',
                url: "/Goviewprojects/",
                sortName: "Id"
            });
            const editFormFields = ref({});
            const editFormOptions = ref([]);
            const searchFormFields = ref({"ProjectName":"","Remarks":""});
            const searchFormOptions = ref([[{"title":"","field":"ProjectName"},{"title":"","field":"Remarks"}]]);
            const columns = ref([{field:'Id',title:'Id',type:'int',sort:true,width:110,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'ProjectName',title:'ProjectName',type:'string',width:110,align:'left',sort:true},
                       {field:'State',title:'State',type:'int',width:110,require:true,align:'left'},
                       {field:'CreateTime',title:'CreateTime',type:'datetime',width:110,align:'left',sort:true},
                       {field:'CreateUserId',title:'CreateUserId',type:'string',width:110,require:true,align:'left'},
                       {field:'IsDelete',title:'IsDelete',type:'int',width:110,require:true,align:'left'},
                       {field:'IndexImage',title:'IndexImage',type:'string',width:110,align:'left'},
                       {field:'Remarks',title:'Remarks',type:'string',width:110,align:'left'}]);
            const detail = ref({
                cnName: "#detailCnName",
                table: "#detailTable",
                columns: [],
                sortName: "",
                key: ""
            });
            return {
                table,
                extend,
                editFormFields,
                editFormOptions,
                searchFormFields,
                searchFormOptions,
                columns,
                detail,
            };
        },
    });
</script>
