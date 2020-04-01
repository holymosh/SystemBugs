<template>
  <v-card>
    <v-card-title>
      <v-select
            :items="systems"
            label="Система"
            dense
            outlined
            attach
            width="50px"
            v-model="selectedSystem"
          >
           <template slot="selection" slot-scope="data">
                                  {{data.item.title}}
                                </template>
                                <template slot="item" slot-scope="data">
                                  {{data.item.title}}
                                </template>
    </v-select>
    <v-spacer></v-spacer>
    <v-select
            :items="levels"
            label="Критичность"
            dense
            outlined
            attach
            width="50px"
            v-model="selectedLevel"
          >
          <template slot="selection" slot-scope="data">
                                  {{data.item.title}}
                                </template>
                                <template slot="item" slot-scope="data">
                                  {{data.item.title}}
                                </template>
    </v-select>
    <v-spacer></v-spacer>
          <v-text-field
            v-model="pickerFrom"
            label="С"
          ></v-text-field>
      <v-spacer></v-spacer>
          <v-text-field
            v-model="pickerTo"
            label="По"
          ></v-text-field>
      <v-spacer></v-spacer>
      <v-text-field
        v-model="search"
        append-icon="mdi-magnify"
        label="Search"
        single-line
        hide-details
      ></v-text-field>
    </v-card-title>
    <v-data-table
      :headers="headers"
      :items="tableData"
      :search="search"
      :items-per-page="5"
    ></v-data-table>
  </v-card>
</template>

<script>
export default {
  data () {
    return {
      search: '',
      headers: [
        {
          text: 'Id',
          align: 'start',
          value: 'id'
        },
        { text: 'Система', value: 'system' },
        { text: 'Описание', value: 'summary' },
        { text: 'Состояние', value: 'state' },
        { text: 'Критичность', value: 'level' },
        { text: 'Тип дефекта', value: 'defectType' },
        { text: 'Создано', value: 'creationDate' },
        { text: 'Изменено', value: 'changeDate' },
        { text: 'Закрыто', value: 'closingDate' },
        { text: 'Обнаружения', value: 'reopensAmount' }
      ],
      tableData: [],
      systems: [],
      levels: [],
      pickerTo: new Date().toLocaleString().substr(0, 10),
      pickerFrom: new Date().toLocaleString().substr(0, 10),
      modal: false,
      selectedSystem: {id:0, title: ''},
      selectedLevel: {id:0, title: ''},
    }
  },
  created () {
    this.initialize()
  },
  methods: {
    initialize () {
      this.getSystemFilter()
      this.getCriticalLevelFilter()
      let url = 'http://localhost:56160/data/get'
      this.loadData(url)
    },
    loadData (url) {
      fetch(url,{
        mode: "cors"
      }).then(r=> r.json()).then(r => {
        r.forEach((item,index,arr) => {
          let current = arr[index];
          current.creationDate = new Date(current.creationDate).toLocaleString()
          current.changeDate = new Date(current.changeDate).toLocaleString()
          if(current.closingDate) current.closingDate = new Date(current.closingDate).toLocaleString()
        });
        this.tableData = r;
      })
    },
    getSystemFilter () {
      let url = 'http://localhost:56160/datasource/system'
      var data = null;
      fetch(url,{
        mode: "cors"
      }).then(r=> r.json()).then(r => this.systems = r)
    },
    getCriticalLevelFilter () {
      let url = 'http://localhost:56160/datasource/level'
      var data = null;
      fetch(url,{
        mode: "cors"
      }).then(r=> r.json()).then(r => this.levels = r)
    }
  }
}
</script>
