<template>
  <div class="customCard card m-4" :style="{background: newBgStyle}">
    <div class="card-header">
      <h4 class="text-center" :style="{textDecoration: newTextStyle}">{{ noteDto.title }}</h4>
      <div class="card-body bg-transparent">
        <p class="card-text" :style="{textDecoration: newTextStyle}" >{{noteDto.description}}</p>
        <h6>{{noteDto.changeTime}}</h6>
        <h6>{{statuses[noteDto.status]}}</h6>
      </div>
      <div class="card-footer text-center">
        <div v-show="actionButtonsVisible===true">
          <button @click="executeNote" class="btnGreen">выполнить</button>
          <button @click="cancelNote" class="btnRed">отменить</button>
        </div>
        <div v-show="restoreButtonsVisible===true">
          <button @click="restoreNote" class="btnRestore">восстановить</button>
          <button @click="deleteNote" class="btnRed">удалить</button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>

export default {
  props: {
    noteDto: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      textStyle: String,
      bgStyle: String,
      statuses: ['В работе','Выполнена','Отменена']
    }
  },
  methods: {
    executeNote() {
      this.$emit("execute", this.noteDto)
    },
    cancelNote() {
      this.$emit("cancel", this.noteDto);
    },
    restoreNote() {
      this.$emit("restore", this.noteDto);
    },
    deleteNote() {
      this.$emit("delete", this.noteDto);
    }
  },
  computed: {
    newTextStyle() {
      return this.noteDto.status === 0 ? '' : 'line-through'
    },
    newBgStyle() {
     if(this.noteDto.status === 1 )
       return 'rgba(152, 251, 152, 0.5)'
      else if(this.noteDto.status === 2)
        return 'rgba(255, 160, 122, 0.5)'
      else return 'rgb(255, 255, 255)'
    },
    actionButtonsVisible() {
      return this.noteDto.status === 0;
    },
    restoreButtonsVisible() {
      return this.noteDto.status === 2;
    }
  }
}
</script>
<style scoped>
.customCard {
  height: auto;
  width: 500px;
  border-radius: 3%;
  border: groove 3px darkgreen;
  box-shadow: 10px 15px 5px grey;
  background-color: transparent;
}
button {
  padding: 10px;
  border-radius: 3%;
  margin: 3px;
}
button:hover {
  scale: 1.1;
}
.btnGreen {
  background: linear-gradient(rgba(152, 251, 152, 0.5), rgba(211, 211, 211, 0.5));
  border: groove 3px darkgreen;
}
.btnRed {
  background: linear-gradient(rgba(205, 92, 92, 0.5),rgba(211, 211, 211, 0.5));
  border: groove 3px darkred;
}
.btnRestore {
  background:linear-gradient(rgba(127, 255, 212, 0.5),rgba(211, 211, 211, 0.5));
}

</style>