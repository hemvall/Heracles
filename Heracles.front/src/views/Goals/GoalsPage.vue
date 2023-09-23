<template>
    <div class="wholePage">
        <div class="subHeader">
            <div class="subHeaderCont">
                <div class="subHeadStates">
                    <a v-if="tabindex == 1" class="state stateFocus leftBtns">Open ({{ goalsActive.length }}) </a>
                    <a v-else class="state leftBtns" @click="tabindex = 1">Open ({{ goalsActive.length }}) </a>
                    <a v-if="tabindex == 2" class="state stateFocus leftBtns">Closed ({{ goalsnActive.length }}) </a>
                    <a v-else class="state leftBtns" @click="tabindex = 2">Closed ({{ goalsnActive.length }}) </a>
                    <a v-if="tabindex == 3" class="state stateFocus leftBtns">All ({{ goals.length }}) </a>
                    <a v-else class="state leftBtns" @click="tabindex = 3">All ({{ goals.length }}) </a>
                    <!-- <a class="state btnGoal">Add a goal </a> -->
                </div>
            </div>
        </div><br>
        <a v-if="tabindex == 1 && goalsActive < 1" class="notFoundTxt">No current goal found.</a>
        <a v-if="tabindex == 2 && goalsnActive < 1" class="notFoundTxt">No closed goal found.</a>
        <a v-if="tabindex == 3 && goals.length < 1" class="notFoundTxt">No goal found at all.</a>
        <div v-if="tabindex == 1">
            <div v-for="g in goalsActive" :key="g.id">
                <div class="goalLine">
                    <div class="layer">
                        <a class="goalTitle">{{ g.label }}</a>
                        <progress value="60" max="100" class="goalProgress"></progress>
                        <a @click="closeGoal(g.id, g.userId, g.exerciseId, g.label, g.data, g.startingDate, g.deadline, g.isActive)"
                            class="closeGoal">Close goal</a>
                    </div>
                    <div class="layer">
                        <a class="goalDeadline">{{ g.startingDate }} -> {{ g.deadline }} <a
                                v-for="dLeft in daysLeft(g.startingDate, g.deadline)" :key="dLeft"> ({{ dLeft }})</a></a>
                        <div class="goalData"><a class="dataTxt">X records</a> &#183 <a class="dataTxt">X% complete</a>
                        </div>
                    </div>
                </div>
            </div>
            <div @click="$router.push(`/goal/create`)" class="goalLine addGoal">
                <div class="layer">
                    <a style="font-weight: 600;" class=""> + Add a new goal</a>
                </div>
            </div>
        </div>
        <div v-if="tabindex == 2">
            <div v-for="g in goalsnActive" :key="g.id">
                <div class="goalLine">
                    <div class="layer">
                        <a class="goalTitle">{{ g.label }}</a>
                        <progress value="60" max="100" class="goalProgress"></progress>
                    </div>
                    <div class="layer">
                        <a class="goalDeadline">{{ g.startingDate }} -> {{ g.deadline }}<a
                                v-for="dLeft in daysLeft(g.startingDate, g.deadline)" :key="dLeft"> ({{ dLeft }})</a></a>
                        <div class="goalData"><a class="dataTxt">X records</a> &#183 <a class="dataTxt">X% complete</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="tabindex == 3">
            <div v-for="g in goals" :key="g.id">
                <div class="goalLine">
                    <div class="layer">
                        <a class="goalTitle">{{ g.label }}</a>
                        <a class="label green" v-if="g.isActive">Current</a>
                        <a class="label red" v-else>Closed</a>
                        <progress value="60" max="100" class="goalProgress"></progress>
                        <a v-if="g.isActive" class="closeGoal">Close goal</a>
                    </div>
                    <div class="layer">
                        <a class="goalDeadline">{{ g.startingDate }} -> {{ g.deadline }} <a
                                v-for="dLeft in daysLeft(g.startingDate, g.deadline)" :key="dLeft"> ({{ dLeft }})</a></a>
                        <div class="goalData"><a class="dataTxt">X records</a> &#183 <a class="dataTxt">X% complete</a>
                        </div>
                    </div>
                </div>
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
            userId: 99,
            tabindex: 1,
            label: '',
            editLabel: '',
            addCategoryOpen: false,
            addExerciseOpen: false,
            editCategoryOpen: false,
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
        closeGoal(gId, uId, eId, lbl, dta, sd, dl, isA) {
            if (confirm("Do you really want to close this goal ?")) {
                const requestOptions = {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        id: gId,
                        isActive: !isA,
                        exerciseId: eId,
                        userId: uId,
                        label: lbl,
                        data: dta,
                        startingDate: sd,
                        deadline: dl
                    })
                };
                fetch(`${this.$api}/goals/${gId}`, requestOptions)
                    .then(response => {
                        if (response.ok) { alert("L'objectif a bien été fermé.") }
                        else { alert("L'objectif a bien été fermé.") }
                        this.fetchData()
                    })
                    .then(response => response.json())
            }
        },
        daysLeft(startingDate, deadline) {
            const startDate = new Date(startingDate);
            const endDate = new Date(deadline);
            const currentTime = new Date();

            if (currentTime >= endDate) {
                return ['Goal Expired'];
            }

            const timeDifference = endDate - currentTime;
            const daysLeft = Math.ceil(timeDifference / (24 * 60 * 60 * 1000));

            return [daysLeft + ' days left'];
        },
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
@import url('./GoalsPage.scss');

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