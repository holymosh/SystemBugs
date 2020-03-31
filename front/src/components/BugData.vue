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
          >
    </v-select>
    <v-spacer></v-spacer>
    <v-select
            :items="systems"
            label="Критичность"
            dense
            outlined
            attach
            width="50px"
          >
    </v-select>
    <v-spacer></v-spacer>
          <v-text-field
            v-model="pickerFrom"
            label="С"
          ></v-text-field>
      <v-spacer></v-spacer>
          <v-text-field
            v-model="pickerFrom"
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
        { text: 'Критичность', value: 'criticalLevel' },
        { text: 'Тип дефекта', value: 'defectType' },
        { text: 'Создано', value: 'creationDate' },
        { text: 'Изменено', value: 'changeDate' },
        { text: 'Закрыто', value: 'closingDate' },
        { text: 'Обнаружения', value: 'reopnesAmount' }
      ],
      tableData: [],
      systems: ['1', '2'],
      criticalLevels: ['3', '4'],
      pickerTo: new Date().toLocaleString().substr(0, 10),
      pickerFrom: new Date().toLocaleString().substr(0, 10),
      modal: false
    }
  },
  created () {
    this.initialize()
  },
  methods: {
    initialize () {
      this.getSystemFilter()
      this.getCriticalLevelFilter()
      this.loadData()
    },
    loadData () {
      console.log('loading.......')
    },
    getSystemFilter () {
      console.log('http req for systems')
    },
    getCriticalLevelFilter () {
      console.log('http req for critical level')
    }
  }
}
</script>
