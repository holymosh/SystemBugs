<template>
  <v-card>
    <v-card-title>
      <v-select
            :items="systems"
            label="Система"
            dense
            outlined
            attach
            style="width: 40px"
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
            style="width: 60px"
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
      <div class="my-2">
        <v-btn @click="apply" small>Filter</v-btn>
      </div>
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

    <v-sparkline
    style="width: 50%"
    :value="value"
    :gradient="gradient"
    :smooth="radius || false"
    :padding="padding"
    :line-width="width"
    :stroke-linecap="lineCap"
    :gradient-direction="gradientDirection"
    :fill="fill"
    :type="type"
    :auto-line-width="autoLineWidth"
    auto-draw
  ></v-sparkline>
  </v-card>
</template>

<script>
const gradients = [
    ['#222'],
    ['#42b3f4'],
    ['red', 'orange', 'yellow'],
    ['purple', 'violet'],
    ['#00c6ff', '#F0F', '#FF0'],
    ['#f72047', '#ffd200', '#1feaea'],
  ]
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
      dataSourceBaseUrl: 'http://localhost:56160/data/get',
      tableData: [],
      systems: [],
      levels: [],
      pickerTo: new Date().toLocaleString().substr(0, 10),
      pickerFrom: new Date().toLocaleString().substr(0, 10),
      modal: false,
      selectedSystem: {id:0, title: ''},
      selectedLevel: {id:0, title: ''},
      width: 2,
      radius: 10,
      padding: 8,
      lineCap: 'round',
      gradient: gradients[5],
      value: [],
      gradientDirection: 'top',
      gradients,
      fill: false,
      type: 'trend',
      autoLineWidth: false,
    }
  },
  created () {
    this.initialize()
  },
  methods: {
    initialize () {
      this.getSystemFilter();
      this.getCriticalLevelFilter();
      this.loadData(this.dataSourceBaseUrl);
    },
    loadData (url) {
      fetch(url,{
        mode: "cors"
      }).then(r=> r.json()).then(r => {
        r.forEach((item) => {
          item.creationDate = new Date(item.creationDate).toLocaleString()
          item.changeDate = new Date(item.changeDate).toLocaleString()
          if(item.closingDate) item.closingDate = new Date(item.closingDate).toLocaleString()
        });
        this.tableData = r;
        let map = new Map();
        r.map(el => new Date(el.creationDate).getDate())
        .forEach((item) => map.set(item,(map.get(item) || 0)+ 1));
        this.value = [...map.keys()].sort(function(a,b){ return a-b}).map(el => map.get(el))
      })
    },
    getSystemFilter () {
      let url = 'http://localhost:56160/datasource/system';
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
    },
    apply () {
      let queryStr = '?';
      let selectedSystem = this.selectedSystem;
      let selectedLevel = this.selectedLevel;
      let pickerTo = this.pickerTo;
      let pickerFrom = this.pickerFrom;
      if (selectedSystem.id) {
        queryStr = queryStr + 'systemId='+selectedSystem.id.toString() + '&'
      }
      if (selectedLevel.id) {
        queryStr = queryStr + 'levelId='+selectedLevel.id.toString() + '&'
      }
      if (pickerFrom) {
        queryStr = queryStr + 'from='+ getDate(pickerFrom) + '&'
      }
      if (pickerTo) {
        queryStr = queryStr + 'to='+ getDate(pickerTo)
      }
      let url = this.dataSourceBaseUrl + queryStr;
      this.loadData(url);
      function getDate(picker) {
        let arr = picker.split('.');
        return `${arr[1]}.${arr[0]}.${arr[2]}`
      }
    }
  }
}
</script>
