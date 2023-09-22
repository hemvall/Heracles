<template>
    <div class="wholePage">
        <h1 style="font-size: 46px;">Leaderboard</h1>
        <div v-for="(u, index) in leaderboard">
            <h1>{{ index + 1 }}</h1>
            <a>{{ u }}</a>
        </div>
    </div>
</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
        this.idUser = localStorage.getItem('userId')
        console.log(this.userId)
    },
    data() {
        return {
            leaderboard: []
        };
    },
    methods: {
        fetchData() {
            this.leaderboard = [];
            this.loading = true;

            fetch(`${this.$api}/leaderboard`)
                .then(r => r.json())
                .then(json => {
                    this.leaderboard = json;
                    this.loading = false;
                    return;
                });
        }
    }
}
);
</script>

<style scoped lang="scss">
@import url('./LeaderBoard.scss');

template {
    margin: 0 10%;
}

.wholePage {
    margin-left: 22%;
    margin-top: 8%;
    color: white;
}

* {
    font-family: Arial, Helvetica, sans-serif;
}
</style>