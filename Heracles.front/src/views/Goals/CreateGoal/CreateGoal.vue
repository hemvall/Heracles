<template>
    <div class="wholePage">
        <div class="subHeader">
            <div class="subHeaderCont">
                <div class="subHeadStates">
                    <h1>Create a new goal</h1>
                </div>
                <hr>
            </div>
        </div><br>
        <div class="formContainer">
            <div class="formSection">
                <h2>What are you trying to achieve ?</h2>
                <input v-model="label" placeholder="ex: Achieve a 150kg deadlift" type="text" />
            </div>
            <div class="formSection">
                <h2>How heavy ?</h2>
                <input v-model="data" placeholder="ex: 150kg / 500m / 2km" type="number" />
            </div>
            <div class="formSection">
                <h2>Start Date</h2>
                <input v-model="startingDate" type="date" />
                <h2>Deadline</h2>
                <input v-model="deadline" type="date" />
            </div>
            <div class="formSection" style="margin-bottom: 3%;">
                <h2>For Which exercise ?</h2>
                <select v-model="exerciseId">
                    <option value="0">- - -</option>
                    <option v-for="e in exercises" :key="e.id" :value="`${e.id}`"> {{ e.label }}</option>
                </select>
            </div>
            <hr>
            <div class="btnsContainer">
                <a class="cancelBtn" @click="$router.go(-1)">Cancel</a>
                <a class="confirmBtn" @click="createGoal">Save changes</a>
            </div>
        </div>
    </div>
</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
        this.userId = localStorage.getItem('userId')
        console.log(this.userId)
    },
    data() {
        return {
            goals: [],
            exercises: [],
            userId: 0,
            tabindex: 1,
            label: '',
            data: '',
            exerciseId: 0,
            startingDate: '',
            deadline: '',
        };
    },
    methods: {
        fetchData() {
            this.exercises = [];
            this.goals = [];
            this.loading = true;

            fetch(`${this.$api}/goals`)
                .then(r => r.json())
                .then(json => {
                    this.goals = json;
                    this.loading = false;
                    return;
                });
            fetch(`${this.$api}/exercises`)
                .then(r => r.json())
                .then(json => {
                    this.exercises = json;
                    this.loading = false;
                    return;
                });
        },
        exercisesFromType(tId) {
            return this.exercises.filter(e => e.typeId == tId && e.userId == localStorage.getItem('userId'))
        },
        createGoal() {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    isActive: true,
                    exerciseId: this.exerciseId,
                    userId: this.userId,
                    label: this.label,
                    data: this.data,
                    startingDate: this.startingDate,
                    deadline: this.deadline
                })
            };
            fetch(`${this.$api}/goals`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("L'objectif a bien été crée.") }
                    else { alert("L'objectif n'a pas été crée.") }
                    this.fetchData();
                    this.$router.push('/goals')
                })
                .then(response => response.json())
        }
    },
    computed: {
        goalsActive() {
            return this.goals.filter(e => e.isActive)
        },
        goalsnActive() {
            return this.goals.filter(e => !e.isActive)
        },
    }
}
);
</script>

<style scoped lang="scss">
@import url('./CreateGoal.scss');

template {
    margin: 0 10%;
}

.wholePage {
    margin-left: 17%;
    margin-top: 8%;
    display: block;
}

* {
    font-family: Arial, Helvetica, sans-serif;
}
</style>